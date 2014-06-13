namespace NJadeTestSuite.Lexer.Tokenizer.Strings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the TokenLocationTests class.
    /// </summary>
    [TestClass]
    public class TokenLocationTests
    {
        /// <summary>
        /// Tests that the token location should create a token at location.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenAtLocation()
        {
            var tokenLocation = new TokenLocation(1, 2, 3);
            var token = tokenLocation.CreateToken(TokenType.Word);

            Assert.AreEqual(TokenType.Word, token.TokenType);
            Assert.AreEqual(1, token.Index);
            Assert.AreEqual(2, token.Line);
            Assert.AreEqual(3, token.Column);
        }

        /// <summary>
        /// Tests that the token location should create a token at location with value.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenAtLocationWithValue()
        {
            var tokenLocation = new TokenLocation(1, 2, 3);
            var token = tokenLocation.CreateToken(TokenType.Word, "hello");

            Assert.AreEqual(TokenType.Word, token.TokenType);
            Assert.AreEqual(1, token.Index);
            Assert.AreEqual(2, token.Line);
            Assert.AreEqual(3, token.Column);
            Assert.AreEqual("hello", token.TokenValue);
            
            Assert.AreEqual("Word - hello", token.ToString());
        }
    }
}
