using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;
using Tests.Common;

namespace UnitTests
{
    [TestClass]
    public class LoanAmountMultipleOfHundredTests
    {
        [TestMethod]
        [DataRow(100)]
        [DataRow(2000)]
        [DataRow(3000)]
        [DataRow(4000)]
        [DataRow(1200)]
        [DataRow(1600)]
        public void Amount_Should_Be_Multiple_Of_Hundred(int amount)
        {
            new LoanRequestedAmountMultipleOfHundredRule().Validate(amount);
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Loan amound should be multiple of 100.")]
        [DataRow(101)]
        [DataRow(1020)]
        public void Not_Multiple_Of_Hundreds_SHould_Throw_Exception(int amount)
        {
            new LoanRequestedAmountMultipleOfHundredRule().Validate(amount);
        }
    }
}
