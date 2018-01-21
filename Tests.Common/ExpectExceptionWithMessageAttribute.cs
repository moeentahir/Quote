using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.Common
{
    public sealed class ExpectExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
    {
        private Type ExpectedExceptionType;
        private string ExpectedExceptionMessage;

        public ExpectExceptionWithMessageAttribute(Type expectedExceptionType)
        {
            ExpectedExceptionType = expectedExceptionType;
            ExpectedExceptionMessage = string.Empty;
        }

        public ExpectExceptionWithMessageAttribute(Type expectedExceptionType, string expectedExceptionMessage)
        {
            ExpectedExceptionType = expectedExceptionType;
            ExpectedExceptionMessage = expectedExceptionMessage;
        }

        protected override void Verify(Exception exception)
        {
            Assert.IsNotNull(exception);

            Assert.IsInstanceOfType(exception, ExpectedExceptionType, "Wrong type of exception was thrown.");

            if (!ExpectedExceptionMessage.Length.Equals(0))
            {
                Assert.AreEqual(ExpectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
            }
        }
    }
}