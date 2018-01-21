using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quote.Common;
using Quote.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace UnitTests
{
    [TestClass]
    public class CsvLenderDataReaderTests
    {
        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "No header record was found.")]
        public async Task When_File_Is_Empty()
        {
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult(""));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Cannot find any lender in the file.")]
        public async Task When_Only_Header_Is_Provided()
        {
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult("Lender,Rate,Available"));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Cannot find any lender in the file.")]
        [DataRow("Lenders,Rate,Available")]
        [DataRow("abc")]
        [DataRow("abc,xyz,123")]
        public async Task No_Data_With_Incorrect_Header_Should_Throw_Exception(string headers)
        {
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult(headers));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "CSV File headers are incorrect. Please make sure you have three headers with names 'Lender', 'Rate' and 'Available'.")]
        public async Task Incorrect_Header_With_Data()
        {
            var expected = 0;
            var data = @"Lenders,Rate,Available
                        Bob,0.075,640";
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult(data));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();
            var actual = lenderData.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Header_With_One_Row()
        {
            var expected = 1;
            var data = @"Lender,Rate,Available
Bob,0.075,640";
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult(data));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();
            var actual = lenderData.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "The data 'asd' you specified in CSV file is not correct type.")]
        public async Task Incorrect_Rate_Should_Throw_Exception()
        {
            var data = @"Lender,Rate,Available
Bob,asd,640";
            var dataReaderMock = new Mock<ILenderRawRateProvider>();

            dataReaderMock.Setup(l => l.Read()).Returns(Task.FromResult(data));

            var lenderData = await new CsvLenderRateDeserializer(dataReaderMock.Object).Deserialize();

        }

    }
}
