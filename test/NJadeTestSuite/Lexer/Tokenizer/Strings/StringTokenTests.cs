namespace NJadeTestSuite.Lexer.Tokenizer.Strings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the StringTokenTests class.
    /// </summary>
    [TestClass]
    public class StringTokenTests
    {
        /// <summary>
        /// Tests that the string token should set properties when constructed without value.
        /// </summary>
        [TestMethod]
        public void ShouldSetPropertiesWhenConstructedWithoutValue()
        {
            var stringToken = new StringToken(TokenType.Word, 1, 1, 1);
            Assert.AreEqual(TokenType.Word, stringToken.TokenType);
            Assert.AreEqual(1, stringToken.Index);
            Assert.AreEqual(1, stringToken.Line);
            Assert.AreEqual(1, stringToken.Column);
            Assert.IsNull(stringToken.TokenValue);
        }

        /// <summary>
        /// Tests that the string token should set properties when constructed with all values.
        /// </summary>
        [TestMethod]
        public void ShouldSetPropertiesWhenConstructedWithAllValues()
        {
            var stringToken = new StringToken(TokenType.Word, 1, 1, 1, "hello");
            Assert.AreEqual(TokenType.Word, stringToken.TokenType);
            Assert.AreEqual(1, stringToken.Index);
            Assert.AreEqual(1, stringToken.Line);
            Assert.AreEqual(1, stringToken.Column);
            Assert.AreEqual("hello", stringToken.TokenValue);
        }
    }
}
