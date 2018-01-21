using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;
using Tests.Common;

namespace UnitTests
{
    [TestClass]
    public class LenderRawDataProviderFromFileTests
    {
        [TestMethod]
        [DataRow("asd.csv")]
        [ExpectExceptionWithMessage(typeof(ValidationException), "The file path 'asd.csv' does not exist")]
        public async Task When_File_Does_Not_Exist(string filePath)
        {
            var fileData = await new LenderRawRateProviderFromFile(filePath).Read();
        }

        [TestMethod]
        [DataRow("LenderData.csv")]
        public async Task File_Four_Lender_Rows(string filePath)
        {

            var fileData = await new LenderRawRateProviderFromFile(filePath).Read();

            Assert.IsTrue(fileData.Contains("Bob"));
        }

        
    }
}
