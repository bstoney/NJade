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
    /// Defines the MatchKeywordTests class.
    /// </summary>
    [TestClass]
    public class MatchKeywordTests
    {
        /// <summary>
        /// Tests that the match keyword should throw an argument exception when the word is an empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowAnArgumentExceptionWhenTheWordIsAnEmptyString()
        {
            new MatchKeyword(TokenType.Word, string.Empty);
        }

        /// <summary>
        /// Tests that the match keyword should throw an argument null exception when the word is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenTheWordIsNull()
        {
            new MatchKeyword(TokenType.Word, null);
        }

        /// <summary>
        /// Tests that the match keyword should throw an argument null exception when the token type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenTheTokenTypeIsNull()
        {
            new MatchKeyword(null, "key");
        }

        /// <summary>
        /// Tests that the match keyword should return a token when the keyword is found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATokenWhenTheKeywordIsFound()
        {
            var stringTokenizer = new StringTokenizer("key");
            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should return null when the keyword is not found.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenTheKeywordIsNotFound()
        {
            var stringTokenizer = new StringTokenizer("kee");
            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should return null when at the end of the tokenizer.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenAtTheEndOfTheTokenizer()
        {
            var stringTokenizer = new StringTokenizer(string.Empty);
            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should allow match to be a substring by default.
        /// </summary>
        [TestMethod]
        public void ShouldAllowMatchToBeASubstringByDefault()
        {
            var matchKeyword = new MatchKeyword(TokenType.Word, "key");
            Assert.IsTrue(matchKeyword.AllowAsSubString);
        }

        /// <summary>
        /// Tests that the match keyword should match a substring when allow as substring is true.
        /// </summary>
        [TestMethod]
        public void ShouldMatchASubstringWhenAllowAsSubstringIsTrue()
        {
            var stringTokenizer = new StringTokenizer("keyword");

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");
            mockMatchKeyword.AllowAsSubString = true;

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should not match a substring when allow as substring is false.
        /// </summary>
        [TestMethod]
        public void ShouldNotMatchASubstringWhenAllowAsSubstringIsFalse()
        {
            var stringTokenizer = new StringTokenizer("keyword");

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");
            mockMatchKeyword.AllowAsSubString = false;

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should match a word terminated by white space when allow as substring is false.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAWordTerminatedByWhiteSpaceWhenAllowAsSubstringIsFalse()
        {
            var stringTokenizer = new StringTokenizer("key word");

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");
            mockMatchKeyword.AllowAsSubString = false;

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should match a word at the end of the tokenizer when allow as substring is false.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAWordAtTheEndOfTheTokenizerWhenAllowAsSubstringIsFalse()
        {
            var stringTokenizer = new StringTokenizer("key");

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");
            mockMatchKeyword.AllowAsSubString = false;

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should match a word terminated by a special character when allow as substring is false.
        /// </summary>
        [TestMethod]
        public void ShouldMatchAWordTerminatedByASpecialCharacterWhenAllowAsSubstringIsFalse()
        {
            var stringTokenizer = new StringTokenizer("key, word");

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");
            mockMatchKeyword.AllowAsSubString = false;
            mockMatchKeyword.SpecialCharacters = new List<MatchKeyword> { new MatchKeyword(TokenType.Word, ",") };

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.IsNotNull(token);
        }

        /// <summary>
        /// Tests that the match keyword should capture current the location.
        /// </summary>
        [TestMethod]
        public void ShouldCaptureCurrentTheLocation()
        {
            var stringTokenizer = new Mock<StringTokenizer>("key") { CallBase = true };

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            mockMatchKeyword.CallImplementation(stringTokenizer.Object);

            stringTokenizer.Verify(t => t.GetCurrentLocation(), Times.Once);
        }

        /// <summary>
        /// Tests that the match keyword should create a token from current the loction.
        /// </summary>
        [TestMethod]
        public void ShouldCreateATokenFromCurrentTheLoction()
        {
            var tokenLocation = new Mock<ITokenLocation>(MockBehavior.Default);
            var stringTokenizer = new Mock<StringTokenizer>("key") { CallBase = true };
            stringTokenizer.Setup(t => t.GetCurrentLocation()).Returns(tokenLocation.Object);

            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            mockMatchKeyword.CallImplementation(stringTokenizer.Object);

            tokenLocation.Verify(l => l.CreateToken(It.IsAny<TokenType>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Tests that the match keyword should set the token type of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheTokenTypeOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("key");
            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.AreEqual(TokenType.Word, token.TokenType);
        }

        /// <summary>
        /// Tests that the match keyword should set the value of the token.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheValueOfTheToken()
        {
            var stringTokenizer = new StringTokenizer("key");
            var mockMatchKeyword = new MockMatchKeyword(TokenType.Word, "key");

            var token = mockMatchKeyword.CallImplementation(stringTokenizer);

            Assert.AreEqual("key", token.TokenValue);
        }

        /// <summary>
        /// Defines the MockMatchKeyword class.
        /// </summary>
        private class MockMatchKeyword : MatchKeyword
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MockMatchKeyword"/> class.
            /// </summary>
            /// <param name="tokenType">The type.</param>
            /// <param name="keyword">The keyword.</param>
            public MockMatchKeyword(TokenType tokenType, string keyword)
                : base(tokenType, keyword)
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
