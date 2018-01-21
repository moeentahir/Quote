using Quote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class CompoundInterestCalculator : IInterestCalculator
    {
        public decimal Calculate(InterestCalculationParameters parameters)
        {
            var middlePart =  1 + (parameters.Rate / (decimal)parameters.CompoundsPerYear);

            var power = parameters.YearsBorrowed * parameters.CompoundsPerYear;

            var lastPart= (decimal)Math.Pow((double)middlePart, power);

            return (parameters.PrincipalAmount * lastPart).RoundTo(2); // Client requirement to round to 2 decimal places
        }
    }
}
