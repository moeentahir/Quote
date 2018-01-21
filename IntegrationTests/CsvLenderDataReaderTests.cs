using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Framework;

namespace IntegrationTests
{
    [TestClass]
    public class CsvLenderDataReaderTests
    {
        [TestMethod]
        [DataRow("LenderData.csv")]
        public async Task Read_COntent_From_File(string filePath)
        {
            var dataProvider = new LenderRawRateProviderFromFile(filePath);

            var loanData = await new CsvLenderRateDeserializer(dataProvider).Deserialize();

            var expected = 7;

            var actual = loanData.Count();

            Assert.AreEqual(expected, actual);

        }
    }
}
