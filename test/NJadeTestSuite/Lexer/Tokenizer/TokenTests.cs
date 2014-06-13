namespace NJadeTestSuite.Lexer.Tokenizer
{
    using Lexer.Tokenizer;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the TokenTests class.
    /// </summary>
    [TestClass]
    public class TokenTests
    {
        /// <summary>
        /// Tests that the token should return formatted description when to string is called.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFormattedDescriptionWhenToStringIsCalled()
        {
            var token = new Token<object>(TokenType.Word, 0, "hello");

            Assert.AreEqual("Word - hello", token.ToString());
        }
    }
}
