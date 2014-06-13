namespace NJadeTestSuite.Lexer.Matcher.Strings
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Lexer.Matcher.Strings;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchWordTests class.
    /// </summary>
    [TestClass]
    public class MatchWordTests
    {
        /// <summary>
        /// Tests that the match word should throw an argument null exception when special characters is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenSpecialCharactersIsNull()
        {
            new MatchWord(null);
        }

        /// <summary>
        /// Tests that the match word should return a token when a word is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATokenWhenAWordIsFound()
        {
            var stringTokenizer = new StringTokenizer("hello");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match word should return null when a white space character is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenAWhiteSpaceCharacterIsFound()
        {
            var stringTokenizer = new StringTokenizer(" hello");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match word should return null when a special character is found instead.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenASpecialCharacterIsFoundInstead()
        {
            var stringTokenizer = new StringTokenizer(",hello");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase> { new MatchKeyword(TokenType.Word, ",") });

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match word should return null when at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new StringTokenizer(string.Empty);
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match word should stop consuming charcaters at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldStopConsumingCharcatersAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new Mock<StringTokenizer>("hello") { CallBase = true };

            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            mockMatchWord.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.Consume(), Times.Exactly(5));
            stringTokenizer.Verify(t => t.IsAtEnd(), Times.AtLeastOnce);
            Assert.IsTrue(stringTokenizer.Object.IsAtEnd());
        }

        /// <summary>
        /// Tests that the match word should match all characters up to a white space character.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAllCharactersUpToAWhiteSpaceCharacter()
        {
            var stringTokenizer = new StringTokenizer("hello ");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match word should match all character up to a special character.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAllCharacterUpToASpecialCharacter()
        {
            var stringTokenizer = new StringTokenizer("hello,");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase> { new MatchKeyword(TokenType.Word, ",") });

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match word should capture current the location.
        /// </summary>
        [TestMethod]
        public void ShouldCaptureCurrentTheLocation()
        {
            var stringTokenizer = new Mock<StringTokenizer>("hello") { CallBase = true };

            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            mockMatchWord.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.GetCurrentLocation(), Times.Once);
        }

        /// <summary>
        /// Tests that the match word should create a token from current the loction.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenFromCurrentTheLoction()
        {
            var tokenLocation = new Mock<ITokenLocation>(MockBehavior.Default);
            var stringTokenizer = new Mock<StringTokenizer>("hello") { CallBase = true };
            stringTokenizer.Setup(t => t.GetCurrentLocation()).Returns(tokenLocation.Object);

            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            mockMatchWord.CallImplementation(stringTokenizer.Object);

            tokenLocation.Verify(l => l.CreateToken(It.IsAny<TokenType>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Tests that the match word should set the token type of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheTokenTypeOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("hello");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.AreEqual(TokenType.Word, token.TokenType);
        }

        /// <summary>
        /// Tests that the match word should set the value of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheValueOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("hello");
            var mockMatchWord = new MockMatchWord(new List<StringMatcherBase>());

            var token = mockMatchWord.CallImplementation(stringTokenizer);

            Assert.AreEqual("hello", token.TokenValue);
        }

        /// <summary>
        /// Defines the MockMatchWord class.
        /// </summary>
        private class MockMatchWord : MatchWord
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MockMatchWord"/> class.
            /// </summary>
            /// <param name="specialCharacters">The special characters.</param>
            public MockMatchWord(List<StringMatcherBase> specialCharacters)
                : base(specialCharacters)
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
