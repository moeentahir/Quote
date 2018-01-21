using Quote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanQuoteProcessor
    {
        private const int NumberOfMonthsLoanFor = 36;

        ILoanRequestValidator Validator { get; }
        ILenderRateDeserializer LenderRatesSerializer { get; }
        public IInterestCalculator InterestCalculator { get; }
        int LoanRequested { get; }

        public LoanQuoteProcessor(ILoanRequestValidator validator, ILenderRateDeserializer lenderRatesSerializer, IInterestCalculator interestCalculator, int loanAmount)
        {
            Validator = validator;
            LenderRatesSerializer = lenderRatesSerializer;
            InterestCalculator = interestCalculator;
            LoanRequested = loanAmount;
        }

        public async Task<LoanQuote> Process()
        {
            Validator.Validate(LoanRequested);

            //Step 1: Get all rates 
            var lenderRates = await LenderRatesSerializer.Deserialize();


            // STep 2: Create breakdown of what amount could be lended from what Lender
            var breakdown = CreateBreakDown(lenderRates);

            decimal weightedAverageRate = CalculateWeightedAverage(breakdown);

            //Step 4: Calculate Interest
            var totalPayable = InterestCalculator.Calculate(new InterestCalculationParameters
            {
                CompoundsPerYear = 12,
                PrincipalAmount = this.LoanRequested,
                Rate = weightedAverageRate,
                YearsBorrowed = NumberOfMonthsLoanFor / 12
            });

            return new LoanQuote
            {
                LoanRequested = LoanRequested,
                Rate = weightedAverageRate,
                MonthlyRepayment = (totalPayable / NumberOfMonthsLoanFor).RoundTo(2),
                Total = totalPayable
            };
        }

        internal decimal CalculateWeightedAverage(IEnumerable<LoanBreakDown> breakdown)
        {
            var weightedSum = breakdown.Sum(b => b.Amount * b.Rate);
            var amountSum = breakdown.Sum(b => b.Amount);

            return (weightedSum / amountSum).RoundTo(3);// client requirement to round 1 decimal when rate represented as percentage e.g 7.5%
        }

        /// <summary>
        /// Creates breakdown of what amount could be lended from what Lender
        /// </summary>
        internal IEnumerable<LoanBreakDown> CreateBreakDown(IEnumerable<LenderRate> lenderRates)
        {
            var maximumLoanAvailable = lenderRates.Sum(r => r.Available);
            if (maximumLoanAvailable < this.LoanRequested)
                throw new ValidationException($"Cannot give quote for the loan amount you requested. The maximum loan you can request is {maximumLoanAvailable}.");

            lenderRates = lenderRates.OrderBy(r => r.Rate);

            var result = new List<LoanBreakDown>();
            decimal loanAmountLeft = LoanRequested;

            foreach (var data in lenderRates)
            {
                if (loanAmountLeft <= 0) break;

                var loanFromCurrentLender = Math.Min(loanAmountLeft, data.Available);
                result.Add(new LoanBreakDown
                {
                    Amount = loanFromCurrentLender,
                    Rate = data.Rate
                });

                loanAmountLeft = loanAmountLeft - loanFromCurrentLender;
            }

            return result;
        }

    }
}
