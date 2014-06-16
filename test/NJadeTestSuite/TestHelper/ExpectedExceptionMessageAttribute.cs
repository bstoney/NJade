namespace NJadeTestSuite.TestHelper
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines the ExpectedExceptionMessageAttribute class.
    /// </summary>
    internal class ExpectedExceptionMessageAttribute : ExpectedExceptionBaseAttribute
    {
        /// <summary>
        /// The expected exception type
        /// </summary>
        private readonly Type expectedExceptionType;

        /// <summary>
        /// The expected exception message
        /// </summary>
        private readonly string expectedExceptionMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectedExceptionMessageAttribute"/> class.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <param name="expectedExceptionMessage">The expected exception message.</param>
        public ExpectedExceptionMessageAttribute(Type expectedExceptionType, string expectedExceptionMessage)
        {
            this.expectedExceptionType = expectedExceptionType;
            this.expectedExceptionMessage = expectedExceptionMessage;
        }

        /// <summary>
        /// Determines whether the exception thrown is the expected exception.
        /// </summary>
        /// <param name="exception">The exception that is thrown by the unit test.</param>
        protected override void Verify(Exception exception)
        {
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, this.expectedExceptionType, "Wrong type of exception was thrown.");
            Assert.AreEqual(this.expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
        }
    }
}
