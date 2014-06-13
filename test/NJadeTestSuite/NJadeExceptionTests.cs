namespace NJadeTestSuite
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade;

    /// <summary>
    /// Defines the NJadeExceptionTests class.
    /// </summary>
    [TestClass]
    public class NJadeExceptionTests
    {
        /// <summary>
        /// Tests that the NJade exception should capture the exception message.
        /// </summary>
        [TestMethod]
        public void ShouldCaptureTheExceptionMessage()
        {
            var exception = new NJadeException("A message.");

            Assert.AreEqual("A message.", exception.Message);
        }
    }
}
