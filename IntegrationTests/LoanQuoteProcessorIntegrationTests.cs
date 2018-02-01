using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote.Common;
using Quote.Framework;
using Tests.Common;

namespace IntegrationTests
{
    [TestClass]
    public class LoanQuoteProcessorIntegrationTests
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(800)]
        [DataRow(-1000)]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Loan amount should be greater than or equal to £1,000.")]
        public async Task Loan_Request_Below_Lower_Limit_Should_Throw_Exception(int loanRequest)
        {
            var args = new[] { "LenderData.CSV", loanRequest.ToString() };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();
        }

        [TestMethod]
        [DataRow(15100)]
        [DataRow(16000)]
        [DataRow(20000)]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Loan amount should be less than or equal to £15,000.")]
        public async Task Loan_Request_Above_Upper_Limit_Should_Throw_Exception(int loanRequest)
        {
            var args = new[] { "LenderData.CSV", loanRequest.ToString() };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();
        }

        [TestMethod]
        [DataRow(1000, ".07", "34.25", "1232.93")]
        [DataRow(2000, ".073", "69.11", "2488.01")]
        public async Task Lender_Data_File_Valid_Requests(
            int loanRequested,
            string expectedRateString,
            string expectedMonthlyPaymentString,
            string expectedTotalString
            )
        {
            var expectedRate = decimal.Parse(expectedRateString);
            var expectedMonthlyPayment = decimal.Parse(expectedMonthlyPaymentString);
            var expectedTotal = decimal.Parse(expectedTotalString);

            var args = new[] { "LenderData.CSV", loanRequested.ToString() };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();

            Assert.AreEqual(loanRequested, actual.LoanRequested);
            Assert.AreEqual(expectedRate, actual.Rate);
            Assert.AreEqual(expectedMonthlyPayment, actual.MonthlyRepayment);
            Assert.AreEqual(expectedTotal, actual.Total);
        }


        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "CSV File headers are incorrect. Please make sure you have three headers with names 'Lender', 'Rate' and 'Available'.")]
        public async Task Incorrect_CSV_Header_Should_Throw_Exception()
        {
            var args = new[] { "IncorrectHeader.CSV", "1000" };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "The data 'asd' you specified in CSV file is not correct type.")]
        public async Task Incorrect_Rate_Type_Should_Throw_Exception()
        {
            var args = new[] { "IncorrectRate.CSV", "1000" };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();
        }


        [TestMethod]
        [DataRow(1000, ".075", "34.76", "1251.45")]
        [DataRow(2000, ".075", "69.52", "2502.89")]
        public async Task One_Lender_File_Valid_Requests(
            int loanRequested,
            string expectedRateString,
            string expectedMonthlyPaymentString,
            string expectedTotalString
            )
        {
            var expectedRate = decimal.Parse(expectedRateString);
            var expectedMonthlyPayment = decimal.Parse(expectedMonthlyPaymentString);
            var expectedTotal = decimal.Parse(expectedTotalString);

            var args = new[] { "OneLender.CSV", loanRequested.ToString() };
            var request = new LoanRequestBuilder(args).Build();

            var rawDataProvider = new LenderRawRateProviderFromFile(request.FilePath);

            var actual = await new LoanQuoteProcessor(
                new LoanRequestedAmountValidator(),
                new CsvLenderRateDeserializer(rawDataProvider),
                new CompoundInterestCalculator(),
                request.LoanAmount)
                .Process();

            Assert.AreEqual(loanRequested, actual.LoanRequested);
            Assert.AreEqual(expectedRate, actual.Rate);
            Assert.AreEqual(expectedMonthlyPayment, actual.MonthlyRepayment);
            Assert.AreEqual(expectedTotal, actual.Total);
        }

    }
}
