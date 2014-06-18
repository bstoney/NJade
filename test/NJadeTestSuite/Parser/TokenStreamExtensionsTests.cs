namespace NJadeTestSuite.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade;
    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TokenStreamExtensionsTests class.
    /// </summary>
    [TestClass]
    public class TokenStreamExtensionsTests
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

        /// <summary>
        /// Tests that the token extensions should raise an unexpected end of template exception when raising an unexpcected token exception and the token stream is empty.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected end of template.")]
        public void ShouldRaiseAnUnexpectedEndOfTemplateExceptionWhenRaisingAnUnexpcectedTokenExceptionAndTheTokenStreamIsEmpty()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(true);
            tokens.Object.RaiseUnexpectedToken();
        }

        /// <summary>
        /// Tests that the token extensions should raise an invalid token exception when raising an unexpcected token exception and the token is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Invalid token.")]
        public void ShouldRaiseAnInvalidTokenExceptionWhenRaisingAnUnexpcectedTokenExceptionAndTheTokenIsNull()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns((StringToken)null);
            tokens.Object.RaiseUnexpectedToken();
        }

        /// <summary>
        /// Tests that the token extensions should get the next token value.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheNextTokenValue()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.AreEqual("hello", tokens.Object.Get());
            tokens.Verify(t => t.Consume(), Times.Once);
        }

        /// <summary>
        /// Tests that the token extensions should return null when getting the next token value and the stream is empty.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenGettingTheNextTokenValueAndTheStreamIsEmpty()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(true);
            Assert.IsNull(tokens.Object.Get());
            tokens.Verify(t => t.Consume(), Times.Never);
        }

        /// <summary>
        /// Tests that the token extensions should raise an invalid token exception when trying to get the next token value and the token is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Invalid token.")]
        public void ShouldRaiseAnInvalidTokenExceptionWhenTryingToGetTheNextTokenValueAndTheTokenIsNull()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns((StringToken)null);
            tokens.Object.Get();
        }

        /// <summary>
        /// Tests that the token extensions should get the next token value when the token type is specified.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheNextTokenValueWhenTheTokenTypeIsSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.AreEqual("hello", tokens.Object.Get(TokenType.Word));
            tokens.Verify(t => t.Consume(), Times.Once);
        }

        /// <summary>
        /// Tests that the token extensions should raise an invalid token exception when trying to get the next token and the token does not match the token type specified.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected hello at line: 1, column: 1.")]
        public void ShouldRaiseAnUnexpectedTokenExceptionWhenTryingToGetTheNextTokenAndTheTokenDoesNotMatchTheTokenTypeSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(false);
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            tokens.Object.Get(TokenType.QuotedString);
        }

        /// <summary>
        /// Tests that the token extensions should get every token until a new line is reached.
        /// </summary>
        [TestMethod]
        public void ShouldGetEveryTokenUntilANewLineIsReached()
        {
            var tokens = new Mock<TokenStream>((IEnumerable<StringToken>)new[]
                                                   {
                                                       new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                       new StringToken(TokenType.WhiteSpace, 1, 1, 1, "hello"),
                                                       new StringToken(JadeTokenType.NewLine, 1, 1, 1, "hello")
                                                   });
            tokens.CallBase = true;
            Assert.AreEqual("hellohello", tokens.Object.GetLine());
            tokens.Verify(t => t.Consume(), Times.Exactly(3));
        }

        /// <summary>
        /// Tests that the token extensions should get every token until the stream is empty.
        /// </summary>
        [TestMethod]
        public void ShouldGetEveryTokenUntilTheStreamIsEmpty()
        {
            var tokens = new Mock<TokenStream>((IEnumerable<StringToken>)new[]
                                                   {
                                                       new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                       new StringToken(TokenType.WhiteSpace, 1, 1, 1, "hello")
                                                   });
            tokens.CallBase = true;
            Assert.AreEqual("hellohello", tokens.Object.GetLine());
            tokens.Verify(t => t.Consume(), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that the token extensions should call consume when the token is any of the token types specified.
        /// </summary>
        [TestMethod]
        public void ShouldCallConsumeWhenTheTokenIsAnyOfTheTokenTypesSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            tokens.Object.ConsumeAny(TokenType.WhiteSpace, TokenType.Word);
            tokens.Verify(t => t.Consume(), Times.Once);
        }

        /// <summary>
        /// Tests that the token extensions should raise an unexpected token exception when the token is not any of the token types specified.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected hello at line: 1, column: 1.")]
        public void ShouldRaiseAnUnexpectedTokenExceptionWhenTheTokenIsNotAnyOfTheTokenTypesSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            tokens.Object.ConsumeAny(TokenType.WhiteSpace, TokenType.QuotedString);
        }

        /// <summary>
        /// Tests that the token extensions should return true when the token is any of the token types specified.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenTheTokenIsAnyOfTheTokenTypesSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.IsTrue(tokens.Object.IsAny(TokenType.WhiteSpace, TokenType.Word));
        }

        /// <summary>
        /// Tests that the token extensions should return false when the token is not any of the token types specified.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTheTokenIsNotAnyOfTheTokenTypesSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.IsFalse(tokens.Object.IsAny(TokenType.WhiteSpace, TokenType.QuotedString));
        }

        /// <summary>
        /// Tests that the token extensions should return false when trying to match any of the supplied token types and the token is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTryingToMatchAnyOfTheSuppliedTokenTypesAndTheTokenIsNull()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns((StringToken)null);
            Assert.IsFalse(tokens.Object.IsAny(TokenType.WhiteSpace, TokenType.QuotedString));
        }

        /// <summary>
        /// Tests that the token extensions should return false when trying to match any of the supplied token types and the stream is empty.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTryingToMatchAnyOfTheSuppliedTokenTypesAndTheStreamIsEmpty()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(true);
            Assert.IsFalse(tokens.Object.IsAny(TokenType.WhiteSpace, TokenType.QuotedString));
        }

        /// <summary>
        /// Tests that the token extensions should return true when the token is the token type specified.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenTheTokenIsTheTokenTypeSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.IsTrue(tokens.Object.Is(TokenType.Word));
        }

        /// <summary>
        /// Tests that the token extensions should return false when the token is not the token type specified.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTheTokenIsNotTheTokenTypeSpecified()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1, "hello"));
            Assert.IsFalse(tokens.Object.Is(TokenType.WhiteSpace));
        }

        /// <summary>
        /// Tests that the token extensions should return false when trying to match the supplied token types and the token is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTryingToMatchTheSuppliedTokenTypesAndTheTokenIsNull()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns((StringToken)null);
            Assert.IsFalse(tokens.Object.Is(TokenType.WhiteSpace));
        }

        /// <summary>
        /// Tests that the token extensions should return false when trying to match the supplied token types and the stream is empty.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenTryingToMatchTheSuppliedTokenTypesAndTheStreamIsEmpty()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.IsAtEnd()).Returns(true);
            Assert.IsFalse(tokens.Object.Is(TokenType.WhiteSpace));
        }

        /////// <summary>
        /////// Tests that the token extensions should return an empty list when getting child tokens and the stream is empty.
        /////// </summary>
        ////[TestMethod]
        ////public void ShouldReturnAnEmptyListWhenGettingChildTokensAndTheStreamIsEmpty()
        ////{
        ////    var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
        ////    tokens.Setup(t => t.IsAtEnd()).Returns(true);
        ////    var tokenHierarchy = tokens.Object.GetTokenHierarchy(null);
        ////    Assert.IsNotNull(tokenHierarchy);
        ////    Assert.AreEqual(0, tokenHierarchy.Count());
        ////}

        /////// <summary>
        /////// Tests that the token extensions should return all tokens on a line when getting child tokens and the indent doesnt change.
        /////// </summary>
        ////[TestMethod]
        ////public void ShouldReturnAllTokensOnALineWhenGettingChildTokensAndTheIndentDoesntChange()
        ////{
        ////    var stringTokens = new[]
        ////                           {
        ////                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
        ////                               new StringToken(TokenType.WhiteSpace, 1, 1, 1, "hello"),
        ////                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
        ////                               new StringToken(JadeTokenType.NewLine, 1, 1, 1, "hello"),
        ////                           };
        ////    var tokens = new Mock<TokenStream>((IEnumerable<StringToken>)stringTokens);
        ////    tokens.CallBase = true;
        ////    var tokenHierarchy = tokens.Object.GetTokenHierarchy(null).ToArray();
        ////    Assert.AreEqual(1, tokenHierarchy.Length);
        ////    CollectionAssert.AreEqual(stringTokens, tokenHierarchy[0].Tokens);
        ////}
    }
}
