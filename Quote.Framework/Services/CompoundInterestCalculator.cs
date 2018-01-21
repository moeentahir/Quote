using Quote.Common;
using System;

namespace Quote.Framework
{
    public class CompoundInterestCalculator : IInterestCalculator
    {
        public decimal Calculate(InterestCalculationParameters parameters)
        {

            // Following calculations are based on the formula described on website: https://www.thecalculatorsite.com/articles/finance/compound-interest-formula.php
            //A = P (1 + r/n) (nt)
            //A = the future value of the investment / loan, including interest
            //P = the principal investment amount(the initial deposit or loan amount)
            //r = the annual interest rate(decimal)
            //n = the number of times that interest is compounded per year
            //t = the number of years the money is invested or borrowed for

            var middlePart = 1 + (parameters.Rate / (decimal)parameters.CompoundsPerYear);

            var power = parameters.YearsBorrowed * parameters.CompoundsPerYear;

            var amountAfterApplyingPower = (decimal)Math.Pow((double)middlePart, power);

            return (parameters.PrincipalAmount * amountAfterApplyingPower).RoundTo(2); // Client requirement to round to 2 decimal places
        }
    }
}
