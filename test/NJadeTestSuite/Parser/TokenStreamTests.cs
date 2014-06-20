using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TokenStreamTests class.
    /// </summary>
    [TestClass]
    public class TokenStreamTests
    {
        /// <summary>
        /// Tests that the token stream should set the items when creating a token stream.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheItemsWhenCreatingATokenStream()
        {
            var line = new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") };
            var tokenStream = new TokenStream(line);

            CollectionAssert.AreEqual(line, tokenStream.GetAll());
        }

        /// <summary>
        /// Tests that the token stream should throw an argument null exception when creating a token stream and the line is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: tokens")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingATokenStreamAndTheLineIsNull()
        {
            new TokenStream(null);
        }
    }
}
