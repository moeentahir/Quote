using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quote.Common;
using Quote.Framework;
using Tests.Common;

namespace UnitTests
{
    [TestClass]
    public class LoanQuoteProcessorTests
    {
        [TestInitialize]
        public void Initialize()
        {
            // Setup a validator that always return true
            var validatorMock = new Mock<ILoanRequestedAmountValidator>();
            ValidatorThatReturnsTrue = validatorMock.Object;

            var lenderRateDeserializerMock = new Mock<ILenderRateDeserializer>();
            ValidatorThatReturnsTrue = validatorMock.Object;

            LenderRatesFromZopaExample = new List<LenderRate>() {
                new LenderRate{Lender = "Bob", Rate= 0.075M, Available=640 },
                new LenderRate{Lender = "Jane", Rate= 0.069M, Available=480 },
                new LenderRate{Lender = "Fred", Rate= 0.071M, Available=520 },
                new LenderRate{Lender = "Mary", Rate= 0.104M, Available=170 },
                new LenderRate{Lender = "John", Rate= 0.081M, Available=320 },
                new LenderRate{Lender = "Dave", Rate= 0.074M, Available=140 },
                new LenderRate{Lender = "Angela", Rate= 0.071M, Available=60 }
            };

        }

        public ILoanRequestedAmountValidator ValidatorThatReturnsTrue { get; set; }

        public ILenderRateDeserializer LenderRatesSerializerFromZopaExample { get; set; }

        public List<LenderRate> LenderRatesFromZopaExample { get; set; }

        [TestMethod]
        [DataRow(1000, 2)]
        [DataRow(1100, 4)]
        [DataRow(2300, 7)]
        public void Break_Down_For_Thouusand_Pounds(int loanAmount, int expected)
        {

            var lenderRateDeserializerMock = new Mock<ILenderRateDeserializer>();
            var interestCalculatorMock = new Mock<IInterestCalculator>();

            var processor = new LoanQuoteProcessor(
                ValidatorThatReturnsTrue,
                lenderRateDeserializerMock.Object,
                interestCalculatorMock.Object,
                loanAmount
                 );

            var breakDown = processor.CreateLoanAmountBreakDown(LenderRatesFromZopaExample);

            var actual = breakDown.Count();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(breakDown.Sum(b => b.Amount), loanAmount);
        }

        [TestMethod]
        [ExpectExceptionWithMessage(typeof(ValidationException), "Cannot give quote for the loan amount you requested. The maximum loan you can request is 2300.")]
        public void Break_Down_More_Than_All_Available_Amount_SHould_Throw_Exception()
        {

            var lenderRateDeserializerMock = new Mock<ILenderRateDeserializer>();
            var interestCalculatorMock = new Mock<IInterestCalculator>();

            var processor = new LoanQuoteProcessor(
                ValidatorThatReturnsTrue,
                lenderRateDeserializerMock.Object,
                interestCalculatorMock.Object,
                2500
                 );

            var breakDown = processor.CreateLoanAmountBreakDown(LenderRatesFromZopaExample);

        }

        [TestMethod]
        public void Weighte_Average_Of_Two_Numbers()
        {

            var lenderRateDeserializerMock = new Mock<ILenderRateDeserializer>();
            var interestCalculatorMock = new Mock<IInterestCalculator>();

            var processor = new LoanQuoteProcessor(
                ValidatorThatReturnsTrue,
                lenderRateDeserializerMock.Object,
                interestCalculatorMock.Object,
                1000
                 );

            var breakDown = processor.CalculateWeightedAverage(new List<LoanAmountBreakDown> {
                new LoanAmountBreakDown{ Amount=480, Rate= 0.069M },
                new LoanAmountBreakDown{ Amount=520, Rate= 0.071M }
            });

            var actual = breakDown;
            var expected = .07M;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Weighte_Average_Same_Weight_Should_Calculate_Average_Exact_Half()
        {

            var lenderRateDeserializerMock = new Mock<ILenderRateDeserializer>();
            var interestCalculatorMock = new Mock<IInterestCalculator>();

            var processor = new LoanQuoteProcessor(
                ValidatorThatReturnsTrue,
                lenderRateDeserializerMock.Object,
                interestCalculatorMock.Object,
                1000
                 );

            var breakDown = processor.CalculateWeightedAverage(new List<LoanAmountBreakDown> {
                new LoanAmountBreakDown{ Amount=500, Rate= 0.09M },
                new LoanAmountBreakDown{ Amount=500, Rate= 0.07M }
            });

            var actual = breakDown;
            var expected = .08M;

            Assert.AreEqual(expected, actual);
        }
    }
}
