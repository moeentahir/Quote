using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;

namespace UnitTests
{
    [TestClass]
    public class LoanAmountMultipleOfHundredTests
    {
        [TestMethod]
        [DataRow("101.0", false)]
        [DataRow("100.1", false)]
        [DataRow("100.0", true)]
        [DataRow("1000.0", true)]
        public void Amount_Should_Be_Multiple_Of_Hundred(string amount, bool expected)
        {
            var actual = new LoanRequestMultipleOfHundredRule().IsValid(decimal.Parse(amount));

            Assert.AreEqual(expected, actual);
        }
    }
}
