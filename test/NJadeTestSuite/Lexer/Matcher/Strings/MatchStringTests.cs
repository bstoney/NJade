namespace NJadeTestSuite.Lexer.Matcher.Strings
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Lexer.Matcher.Strings;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchStringTests class.
    /// </summary>
    [TestClass]
    public class MatchStringTests
    {
        /// <summary>
        /// Tests that the match string should throw argument exception when the delimiter is an empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowAnArgumentExceptionWhenTheDelimiterIsAnEmptyString()
        {
            new MatchString(string.Empty);
        }

        /// <summary>
        /// Tests that the match string should throw an argument null exception whenthe delimiter is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhentheDelimiterIsNull()
        {
            new MatchString(null);
        }

        /// <summary>
        /// Tests that the match string should return a token when a string is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATokenWhenAStringIsFound()
        {
            var stringTokenizer = new StringTokenizer("'123'");
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match string should return null when no string is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenNoStringIsFound()
        {
            var stringTokenizer = new StringTokenizer("123");
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match string should return null when at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new StringTokenizer(string.Empty);
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match string should stop consuming after the second delimiter is found.
        /// </summary>
        [TestMethod]
        public void ShouldStopConsumingAfterTheSecondDelimiterIsFound()
        {
            var stringTokenizer = new Mock<StringTokenizer>("'1'2") { CallBase = true };

            var mockMatchString = new MockMatchString("'");

            mockMatchString.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.Consume(), Times.Exactly(3));
            stringTokenizer.Verify(t => t.IsAtEnd(), Times.AtLeastOnce);
            Assert.AreEqual("2", stringTokenizer.Object.Current);
        }

        /// <summary>
        /// Tests that the match string should match an empty string.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAnEmptyString()
        {
            var stringTokenizer = new StringTokenizer("''");
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match string should return null when the closing delimiter is not found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenTheClosingDelimiterIsNotFound()
        {
            var stringTokenizer = new StringTokenizer("'1");
            var matchWhiteSpace = new MockMatchString("'");

            var token = matchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match string should capture current the location.
        /// </summary>
        [TestMethod]
        public void ShouldCaptureCurrentTheLocation()
        {
            var stringTokenizer = new Mock<StringTokenizer>("''") { CallBase = true };

            var mockMatchString = new MockMatchString("'");

            mockMatchString.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.GetCurrentLocation(), Times.Once);
        }

        /// <summary>
        /// Tests that the match string should create a token from current the loction.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenFromCurrentTheLoction()
        {
            var tokenLocation = new Mock<ITokenLocation>(MockBehavior.Default);
            var stringTokenizer = new Mock<StringTokenizer>("'1'") { CallBase = true };
            stringTokenizer.Setup(t => t.GetCurrentLocation()).Returns(tokenLocation.Object);

            var mockMatchString = new MockMatchString("'");

            mockMatchString.CallImplementation(stringTokenizer.Object);

            tokenLocation.Verify(l => l.CreateToken(It.IsAny<TokenType>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Tests that the match string should set the token type of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheTokenTypeOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("'1'");
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.AreEqual(TokenType.QuotedString, token.TokenType);
        }

        /// <summary>
        /// Tests that the match string should set the value of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheValueOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("'1'");
            var mockMatchString = new MockMatchString("'");

            var token = mockMatchString.CallImplementation(stringTokenizer);

            Assert.AreEqual("1", token.TokenValue);
        }

        /// <summary>
        /// Defines the MockMatchString class.
        /// </summary>
        public class MockMatchString : MatchString
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="MockMatchString"/> class.
            /// </summary>
            /// <param name="delimimiter">The delimimiter.</param>
            public MockMatchString(string delimimiter)
                : base(delimimiter)
            {
            }

            /// <summary>
            /// Calls the implementation.
            /// </summary>
            /// <param name="tokenizer">The tokenizer.</param>
            /// <returns>The implementation result.</returns>
            public StringToken CallImplementation(StringTokenizer tokenizer)
            {
                return this.GetTokenImpl(tokenizer);
            }
        }
    }
}
