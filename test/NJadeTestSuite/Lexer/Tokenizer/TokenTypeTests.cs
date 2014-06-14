namespace NJadeTestSuite.Lexer.Tokenizer
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the TokenTypeTests class.
    /// </summary>
    [TestClass]
    public class TokenTypeTests
    {
        /// <summary>
        /// Tests that the token type should be equal when compared.
        /// </summary>
        [TestMethod]
        public void ShouldBeEqualWhenCompared()
        {
            var eof1 = TokenType.Eof;
            var eof2 = TokenType.Eof;
            var word = TokenType.Word;
            Assert.AreEqual(eof1, eof2);
            Assert.IsTrue(eof1 == eof2);
            Assert.IsFalse(eof1 == word);
        }

        /// <summary>
        /// Tests that the token type should return description when to string is called.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDescriptionWhenToStringIsCalled()
        {
            var newTokenType = new TokenType("A description");

            Assert.AreEqual("A description", newTokenType.ToString());
        }

        /// <summary>
        /// Tests that the token type should return the default to string when description is null or empty.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTheDefaultToStringWhenDescriptionIsNullOrEmpty()
        {
            var newTokenType = new TokenType(null);
            Assert.AreEqual(typeof(TokenType).FullName, newTokenType.ToString());
            
            newTokenType = new TokenType(string.Empty);
            Assert.AreEqual(typeof(TokenType).FullName, newTokenType.ToString());
        }
    }
}
