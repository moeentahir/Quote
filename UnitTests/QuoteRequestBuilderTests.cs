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


    }
}
