namespace NJadeTestSuite.Parser
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TokenExtensionsTests class.
    /// </summary>
    [TestClass]
    public class TokenExtensionsTests
    {
        /// <summary>
        /// Tests that the token extensions should raise an unexpcected token exception.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected hello at line: 1, column: 1.")]
        public void ShouldRaiseAnUnexpcectedTokenException()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            tokens.Object.RaiseUnexpectedToken();
        }
    }
}
