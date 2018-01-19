using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quote.Framework;

namespace UnitTests
{
    [TestClass]
    public class LoanQuoteProcessorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var validator = new Mock<ILoanRequestValidator>();
            validator.Setup(v => v.Validate(null)).Returns(true);

            var processor = new LoanQuoteProcessor(validator.Object, new LoanRequest {

            });

        }
    }
}
