namespace NJadeTestSuite.Parser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade;
    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;
    using NJade.Parser.Parsers;

    using NJadeTestSuite.TestHelper;

    [TestClass]
    public class ElementParserTests
    {
        /// <summary>
        /// Tests that the element parser should throw an argument null exception when when pasing the next element and the token line stream is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: lines")]
        public void ShouldThrowAnArgumentNullExceptionWhenWhenPasingTheNextElementAndTheTokenLineStreamIsNull()
        {
            var elementParser = new ElementParser();
            elementParser.ParseNextElement(null);
        }

        /// <summary>
        /// Tests that the element parser should throw an invalid operation exception when when pasing the next element and the token line stream is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowAnInvalidOperationExceptionWhenWhenPasingTheNextElementAndTheTokenLineStreamIsEmpty()
        {
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.IsAtEnd()).Returns(true);

            var elementParser = new ElementParser();
            elementParser.ParseNextElement(lines.Object);
        }

        /// <summary>
        /// Tests that the element parser should throw an unexpected token exception when pasing the next element and there are tokens remaining.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected hello at line: 1, column: 1.")]
        public void ShouldThrowAnUnexpectedTokenExceptionWhenPasingTheNextElementAndThereAreTokensRemaining()
        {
            var tokenLine = new Mock<TokenLine>(Enumerable.Empty<StringToken>());
            tokenLine.Setup(l => l.Current).Returns(new StringToken(TokenType.QuotedString, 1, 1, 1, "hello"));
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.Current).Returns(tokenLine.Object);

            var elementParser = new ElementParser();
            elementParser.ParseNextElement(lines.Object);
        }

        /// <summary>
        /// Tests that the element parser should get the current line when pasing the next element.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheCurrentLineWhenPasingTheNextElement()
        {
            var tokenLine = new Mock<TokenLine>(Enumerable.Empty<StringToken>());
            tokenLine.Setup(l => l.IsAtEnd()).Returns(true);
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.Current).Returns(tokenLine.Object);

            var elementParser = new ElementParser();
            elementParser.ParseNextElement(lines.Object);

            Assert.IsTrue(tokenLine.Object.IsAtEnd());
            lines.Verify(l => l.Current, Times.Once);
        }

        /// <summary>
        /// Tests that the element parser should return null when pasing the next element and there are no elements to parse.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenPasingTheNextElementAndThereAreNoElementsToParse()
        {
            var tokenLine = new Mock<TokenLine>(Enumerable.Empty<StringToken>());
            tokenLine.Setup(l => l.IsAtEnd()).Returns(true);
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.Current).Returns(tokenLine.Object);

            var elementParser = new ElementParser();
            var element = elementParser.ParseNextElement(lines.Object);
            Assert.IsNull(element);
        }

        /// <summary>
        /// Tests that the element parser should return an element when pasing the next element.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAnElementWhenPasingTheNextElement()
        {
            var tokenLine = new Mock<TokenLine>((IEnumerable<StringToken>)new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") });
            tokenLine.CallBase = true;
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.Current).Returns(tokenLine.Object);

            var elementParser = new ElementParser();
            var element = elementParser.ParseNextElement(lines.Object);
            Assert.IsNotNull(element);
            Assert.IsTrue(tokenLine.Object.IsAtEnd());
        }

        /// <summary>
        /// Tests that the element parser should consume the first line when pasing the next element and there are no more tokens in the line.
        /// </summary>
        [TestMethod]
        public void ShouldConsumeTheFirstLineWhenPasingTheNextElementAndThereAreNoMoreTokensInTheLine()
        {
            var tokenLine = new Mock<TokenLine>((IEnumerable<StringToken>)new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") });
            tokenLine.CallBase = true;
            var lines = new Mock<TokenLineStream>(Enumerable.Empty<TokenLine>());
            lines.Setup(l => l.Current).Returns(tokenLine.Object);

            var elementParser = new ElementParser();
            elementParser.ParseNextElement(lines.Object);

            Assert.IsTrue(tokenLine.Object.IsAtEnd());
            lines.Verify(l => l.Consume(), Times.Once);
        }
    }
}
