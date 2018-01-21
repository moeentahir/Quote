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
        private const int CompoundsPerYear = 12;
        private const int MonthsPerYear = 12;

        ILoanRequestedAmountValidator Validator { get; }
        ILenderRateDeserializer LenderRatesDeserializer { get; }
        public IInterestCalculator InterestCalculator { get; }
        int LoanRequested { get; }

        public LoanQuoteProcessor(ILoanRequestedAmountValidator validator, ILenderRateDeserializer lenderRatesDeserializer, IInterestCalculator interestCalculator, int loanAmount)
        {
            Validator = validator;
            LenderRatesDeserializer = lenderRatesDeserializer;
            InterestCalculator = interestCalculator;
            LoanRequested = loanAmount;
        }

        public async Task<LoanQuote> Process()
        {
            Validator.Validate(LoanRequested);

            //Step 1: Get all lender rates 
            var lenderRates = await LenderRatesDeserializer.Deserialize();

            // STep 2: Create breakdown of what amount could be lended from what Lender
            var breakdown = CreateLoanAmountBreakDown(lenderRates);

            // Step 3: Calculate weighted average of lender rates
            decimal weightedAverageRate = CalculateWeightedAverage(breakdown);

            //Step 4: Calculate Interest
            var totalPayable = InterestCalculator.Calculate(new InterestCalculationParameters
            {
                CompoundsPerYear = CompoundsPerYear,
                PrincipalAmount = LoanRequested,
                Rate = weightedAverageRate,
                YearsBorrowed = NumberOfMonthsLoanFor / MonthsPerYear
            });

            return new LoanQuote
            {
                LoanRequested = LoanRequested,
                Rate = weightedAverageRate,
                MonthlyRepayment = (totalPayable / NumberOfMonthsLoanFor).RoundTo(2),
                Total = totalPayable
            };
        }

        internal decimal CalculateWeightedAverage(IEnumerable<LoanAmountBreakDown> breakdown)
        {
            var weightedSum = breakdown.Sum(b => b.Amount * b.Rate);
            var amountSum = breakdown.Sum(b => b.Amount);

            return (weightedSum / amountSum).RoundTo(3);// client requirement to round 1 decimal when rate represented as percentage e.g 7.5%
        }

        /// <summary>
        /// Creates breakdown of what amount could be lended from what Lender
        /// </summary>
        internal IEnumerable<LoanAmountBreakDown> CreateLoanAmountBreakDown(IEnumerable<LenderRate> lenderRates)
        {
            var maximumLoanAvailable = lenderRates.Sum(r => r.Available);
            maximumLoanAvailable = Math.Floor(maximumLoanAvailable / 100.0M) * 100;
            if (maximumLoanAvailable < this.LoanRequested)
                throw new ValidationException($"Cannot give quote for the loan amount you requested. The maximum loan you can request is {maximumLoanAvailable}.");

            // Step 1: Sort lender rates fo find the cheapest first
            lenderRates = lenderRates.OrderBy(r => r.Rate);

            var result = new List<LoanAmountBreakDown>();
            decimal loanAmountLeft = LoanRequested;

            // Step 2: Build loan amount breakdown
            foreach (var data in lenderRates)
            {
                if (loanAmountLeft <= 0) break;

                var loanFromCurrentLender = Math.Min(loanAmountLeft, data.Available);
                result.Add(new LoanAmountBreakDown
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
