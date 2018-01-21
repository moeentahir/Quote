using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Framework;
using Quote.Common;

namespace UnitTests
{
    [TestClass]
    public class CompoundInterestCalculatorTests
    {
        [TestMethod]
        [DataRow(5000, "8235.05")]
        public void Ten_Year_Loan_At_Five_Percent(int principalAmount, string expectedString)
        {
            var expected = decimal.Parse(expectedString);

            var calculator = new CompoundInterestCalculator();

            var actual = calculator.Calculate(new InterestCalculationParameters
            {
                PrincipalAmount = principalAmount,
                Rate = .05M,
                CompoundsPerYear = 12,
                YearsBorrowed = 10
            });


            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [DataRow(1000, "1232.93")]
        public void THree_Year_Loan_At_Seven_Percent(int principalAmount, string expectedString)
        {
            var expected = decimal.Parse(expectedString);

            var calculator = new CompoundInterestCalculator();

            var actual = calculator.Calculate(new InterestCalculationParameters
            {
                PrincipalAmount = principalAmount,
                Rate = .07M,
                CompoundsPerYear = 12,
                YearsBorrowed = 3
            });


            Assert.AreEqual(expected, actual);

        }
    }
}
