namespace NJadeTestSuite.Lexer.Matcher.Strings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Lexer.Matcher.Strings;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchWhiteSpaceTests class.
    /// </summary>
    [TestClass]
    public class MatchWhiteSpaceTests
    {
        /// <summary>
        /// Tests that the match white space should return a token when white space is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATokenWhenWhiteSpaceIsFound()
        {
            var stringTokenizer = new StringTokenizer(" ");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match white space should return null when no white space is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenNoWhiteSpaceIsFound()
        {
            var stringTokenizer = new StringTokenizer("123");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match white space should return null when at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new Mock<StringTokenizer>(string.Empty);
            stringTokenizer.Setup(t => t.IsAtEnd()).Returns(true);
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer.Object);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match white space should treat a space as white space.
        /// </summary>
        [TestMethod]
        public void ShouldTreatASpaceAsWhiteSpace()
        {
            var stringTokenizer = new StringTokenizer(" ");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match white space should treat a tab as white space.
        /// </summary>
        [TestMethod]
        public void ShouldTreatATabAsWhiteSpace()
        {
            var stringTokenizer = new StringTokenizer("\t");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match white space should treat a new line as white space.
        /// </summary>
        [TestMethod]
        public void ShouldTreatANewLineAsWhiteSpace()
        {
            var stringTokenizer = new StringTokenizer("\n");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match white space should treat a carriage return as white space.
        /// </summary>
        [TestMethod]
        public void ShouldTreatACarriageReturnAsWhiteSpace()
        {
            var stringTokenizer = new StringTokenizer("\r");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match white space should stop consuming white space at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldStopConsumingWhiteSpaceAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new Mock<StringTokenizer>(" ") { CallBase = true };

            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            mockMatchWhiteSpace.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.Consume(), Times.Once);
            stringTokenizer.Verify(t => t.IsAtEnd(), Times.AtLeastOnce);
            Assert.IsTrue(stringTokenizer.Object.IsAtEnd());
        }

        /// <summary>
        /// Tests that the match white space should stop consuming white space at a non white space character.
        /// </summary>
        [TestMethod]
        public void ShouldStopConsumingWhiteSpaceAtANonWhiteSpaceCharacter()
        {
            var stringTokenizer = new Mock<StringTokenizer>(" 123") { CallBase = true };

            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            mockMatchWhiteSpace.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.Consume(), Times.Once);
            stringTokenizer.Verify(t => t.IsAtEnd(), Times.AtLeastOnce);
            Assert.AreEqual("1", stringTokenizer.Object.Current);
        }

        /// <summary>
        /// Tests that the match white space should consume all available white space from tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldConsumeAllAvailableWhiteSpaceFromTokenizer()
        {
            var stringTokenizer = new StringTokenizer(" \t\t \r\n ");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
            Assert.IsTrue(stringTokenizer.IsAtEnd());
        }

        /// <summary>
        /// Tests that the match white space should capture the current location.
        /// </summary>
        [TestMethod]
        public void ShouldCaptureCurrentTheLocation()
        {
            var stringTokenizer = new Mock<StringTokenizer>(string.Empty) { CallBase = true };

            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            mockMatchWhiteSpace.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.GetCurrentLocation(), Times.Once);
        }

        /// <summary>
        /// Tests that the match white space should create a token from the current loction.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenFromCurrentTheLoction()
        {
            var tokenLocation = new Mock<ITokenLocation>(MockBehavior.Default);
            var stringTokenizer = new Mock<StringTokenizer>(" ") { CallBase = true };
            stringTokenizer.Setup(t => t.GetCurrentLocation()).Returns(tokenLocation.Object);

            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            mockMatchWhiteSpace.CallImplementation(stringTokenizer.Object);

            tokenLocation.Verify(l => l.CreateToken(It.IsAny<TokenType>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Tests that the match white space should set the token type of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheTokenTypeOfTheToken()
        {
            var stringTokenizer = new StringTokenizer(" ");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.AreEqual(TokenType.WhiteSpace, token.TokenType);
        }

        /// <summary>
        /// Tests that the match white space should set the value of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheValueOfTheToken()
        {
            var stringTokenizer = new StringTokenizer(" ");
            var mockMatchWhiteSpace = new MockMatchWhiteSpace();

            var token = mockMatchWhiteSpace.CallImplementation(stringTokenizer);

            Assert.AreEqual(" ", token.TokenValue);
        }

        /// <summary>
        /// The mock match white space.
        /// </summary>
        private class MockMatchWhiteSpace : MatchWhiteSpace
        {
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
