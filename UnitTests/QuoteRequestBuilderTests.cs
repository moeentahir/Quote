using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;

namespace UnitTests
{
    [TestClass]
    public class QuoteRequestBuilderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void When_Argument_Is_Empty()
        {
            var args = new string[] { };
            var quoteRequest = new LoanRequestBuilder(args).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
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
