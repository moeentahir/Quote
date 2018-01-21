using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Framework;

namespace IntegrationTests
{
    [TestClass]
    public class CsvLenderDataReaderIntegrationTests
    {
        [TestMethod]
        [DataRow("OneLender.csv", 1)]
        [DataRow("LenderData.csv", 7)]
        [DataRow("BigData.csv", 499)]
        public async Task Read_COntent_From_File(string filePath, int expected)
        {
            var dataProvider = new LenderRawRateProviderFromFile(filePath);

            var loanData = await new CsvLenderRateDeserializer(dataProvider).Deserialize();


            var actual = loanData.Count();

            Assert.AreEqual(expected, actual);

        }
    }
}
