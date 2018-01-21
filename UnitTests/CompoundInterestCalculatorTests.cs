using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Framework;

namespace UnitTests
{
    [TestClass]
    public class CompoundInterestCalculatorTests
    {
        [TestMethod]
        public void Interest_On_THousand_Pound()
        {
            var calculator = new CompoundInterestCalculator();

            var actual = calculator.Calculate(new InterestCalculationParameters
            {
                PrincipalAmount = 5000,
                Rate = .05M,
                CompoundsPerYear = 12,
                YearsBorrowed = 10
            });

            var expected = 8235.05M;

            Assert.AreEqual(expected, actual);


        }
    }
}
