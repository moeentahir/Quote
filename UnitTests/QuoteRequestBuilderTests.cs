using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;
using Tests.Common;

namespace UnitTests
{
    [TestClass]
    public class QuoteRequestBuilderTests
    {
        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Two arguments are needed to get the quote. First should be lender data CSV file path and second should be Loan Amount.")]
        public void When_Argument_Is_Empty()
        {
            var args = new string[] { };
            var quoteRequest = new LoanRequestBuilder(args).Build();
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Two arguments are needed to get the quote. First should be lender data CSV file path and second should be Loan Amount.")]
        public void When_Argument_Is_Null()
        {
            var quoteRequest = new LoanRequestBuilder(null).Build();
        }


        [TestMethod]
        public void When_Proper_Arguments_Are_Passed()
        {
            var quoteRequest = new LoanRequestBuilder(new string[] { "Data.csv", "1000" }).Build();

            Assert.AreEqual(quoteRequest.FilePath, "Data.csv");
            Assert.AreEqual(quoteRequest.LoanAmount, 1000M);
        }

    }
}
