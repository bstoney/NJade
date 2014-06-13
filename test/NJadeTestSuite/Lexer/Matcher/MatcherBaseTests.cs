namespace NJadeTestSuite.Lexer.Matcher
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Lexer.Matcher;
    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the MatcherBaseTests class.
    /// </summary>
    [TestClass]
    public class MatcherBaseTests
    {
        /// <summary>
        /// Tests that the matcher base should match when the tokenizer has passed the last item.
        /// </summary>
        [TestMethod]
        public void ShouldMatchWhenTheTokenizerHasPassedTheLastItem()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();
            tokenizer.Setup(t => t.IsAtEnd()).Returns(true);

            var mockMatcher = new MockMatcher(null);

            var isMatch = mockMatcher.IsMatch(tokenizer.Object);

            Assert.IsTrue(isMatch);
            tokenizer.Verify(t => t.IsAtEnd(), Times.Once);
            Assert.AreEqual(0, mockMatcher.ImplementationCalls);
        }

        /// <summary>
        /// Tests that the matcher base should successfully match when the matcher implementation returns a token.
        /// </summary>
        [TestMethod]
        public void ShouldMatchWhenTheMatcherImplementationReturnsAToken()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(new Token<object>(TokenType.Word));

            Assert.IsTrue(mockMatcher.IsMatch(tokenizer.Object));
        }

        /// <summary>
        /// Tests that the matcher base should not match when the implementation returns null.
        /// </summary>
        [TestMethod]
        public void ShouldNotMatchWhenTheImplementationReturnsNull()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            Assert.IsFalse(mockMatcher.IsMatch(tokenizer.Object));
        }

        /// <summary>
        /// Tests that the matcher base should call get token implementation when testing for a match.
        /// </summary>
        [TestMethod]
        public void ShouldCallGetTokenImplementationWhenTestingForAMatch()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.IsMatch(tokenizer.Object);

            Assert.AreEqual(1, mockMatcher.ImplementationCalls);
        }

        /// <summary>
        /// Tests that the matcher base should call is at end when testing for a match.
        /// </summary>
        [TestMethod]
        public void ShouldCallIsAtEndWhenTestingForAMatch()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.IsMatch(tokenizer.Object);

            tokenizer.Verify(t => t.IsAtEnd(), Times.Once);
        }

        /// <summary>
        /// Tests that the matcher base should call take snapshot when testing for a match.
        /// </summary>
        [TestMethod]
        public void ShouldCallTakeSnapshotWhenTestingForAMatch()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.IsMatch(tokenizer.Object);

            tokenizer.Verify(t => t.TakeSnapshot(), Times.Once);
        }

        /// <summary>
        /// Tests that the matcher base should call rollback snapshot when testing for a match.
        /// </summary>
        [TestMethod]
        public void ShouldCallRollbackSnapshotWhenTestingForAMatch()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.IsMatch(tokenizer.Object);

            tokenizer.Verify(t => t.RollbackSnapshot(), Times.Once);
        }

        /// <summary>
        /// Tests that the matcher base should not call commit snapshot when testing for a match.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallCommitSnapshotWhenTestingForAMatch()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.IsMatch(tokenizer.Object);

            tokenizer.Verify(t => t.CommitSnapshot(), Times.Never);
        }

        /// <summary>
        /// Tests that the matcher base should return null when the tokenizer has consumed the last item.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenTheTokenizerHasConsumedTheLastItem()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();
            tokenizer.Setup(t => t.IsAtEnd()).Returns(true);

            var mockMatcher = new MockMatcher(null);

            var token = mockMatcher.GetToken(tokenizer.Object);

            Assert.IsNull(token);
            tokenizer.Verify(t => t.IsAtEnd(), Times.Once);
            Assert.AreEqual(0, mockMatcher.ImplementationCalls);
        }

        /// <summary>
        /// Tests that the matcher base should return a token when the implementation returns a token.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATokenWhenTheImplementationReturnsAToken()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var token = new Token<object>(TokenType.Word);
            var mockMatcher = new MockMatcher(token);

            Assert.AreEqual(token, mockMatcher.GetToken(tokenizer.Object));
            Assert.AreEqual(1, mockMatcher.ImplementationCalls);
        }

        /// <summary>
        /// Tests that the matcher base should return null when the implementation returns null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenTheImplementationReturnsNull()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            Assert.IsNull(mockMatcher.GetToken(tokenizer.Object));
            Assert.AreEqual(1, mockMatcher.ImplementationCalls);
        }

        /// <summary>
        /// Tests that the matcher base should call take snapshot when getting a token.
        /// </summary>
        [TestMethod]
        public void ShouldCallTakeSnapshotWhenGettingAToken()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.GetToken(tokenizer.Object);

            tokenizer.Verify(t => t.TakeSnapshot(), Times.Once);
        }

        /// <summary>
        /// Tests that the matcher base should call commit snapshot when the implementation returns a token.
        /// </summary>
        [TestMethod]
        public void ShouldCallCommitSnapshotWhenTheImplementationReturnsAToken()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(new Token<object>(TokenType.Word));

            mockMatcher.GetToken(tokenizer.Object);

            tokenizer.Verify(t => t.CommitSnapshot(), Times.Once);
            tokenizer.Verify(t => t.RollbackSnapshot(), Times.Never);
        }

        /// <summary>
        /// Tests that the matcher base should call rollback snapshot when the implementation returns null.
        /// </summary>
        [TestMethod]
        public void ShouldCallRollbackSnapshotWhenTheImplementationReturnsNull()
        {
            var tokenizer = new Mock<ITokenizableStreamBase<object>>();

            var mockMatcher = new MockMatcher(null);

            mockMatcher.GetToken(tokenizer.Object);

            tokenizer.Verify(t => t.RollbackSnapshot(), Times.Once);
            tokenizer.Verify(t => t.CommitSnapshot(), Times.Never);
        }

        /// <summary>
        /// Defines the MockMatcher class.
        /// </summary>
        private class MockMatcher : MatcherBase<ITokenizableStreamBase<object>, Token<object>, object>
        {
            /// <summary>
            /// The return token
            /// </summary>
            private readonly Token<object> returnToken;

            /// <summary>
            /// Initializes a new instance of the <see cref="MockMatcher" /> class.
            /// </summary>
            /// <param name="returnToken">The return token.</param>
            public MockMatcher(Token<object> returnToken)
            {
                this.returnToken = returnToken;
            }

            /// <summary>
            /// Gets the implementation calls.
            /// </summary>
            public int ImplementationCalls { get; private set; }

            /// <summary>
            /// Gets the token implementation.
            /// </summary>
            /// <param name="tokenizer">The tokenizer.</param>
            /// <returns>
            /// A token.
            /// </returns>
            protected override Token<object> GetTokenImpl(ITokenizableStreamBase<object> tokenizer)
            {
                this.ImplementationCalls++;
                return this.returnToken;
            }
        }
    }
}
