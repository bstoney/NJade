namespace NJadeTestSuite.Lexer.Tokenizer
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the TokenTests class.
    /// </summary>
    [TestClass]
    public class TokenTests
    {
        /// <summary>
        /// Tests that the token should throw an argument null exception if token type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionIfTokenTypeIsNull()
        {
            new Token<object>(null);
        }

        /// <summary>
        /// Tests that the token should return formatted description when to string is called.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFormattedDescriptionWhenToStringIsCalled()
        {
            var token = new Token<object>(TokenType.Word, 0, "hello");
            Assert.AreEqual("Word - hello", token.ToString());
        }

        /// <summary>
        /// Tests that the token should return the token type when set.
        /// </summary>
        [TestMethod]
        public void ShouldSetProperiesWhenConstructedWithTokenType()
        {
            var token = new Token<object>(TokenType.Word);
            Assert.AreEqual(TokenType.Word, token.TokenType);
            Assert.IsNull(token.Index);
            Assert.IsNull(token.TokenValue);
        }

        /// <summary>
        /// Tests that the token should return index when set.
        /// </summary>
        [TestMethod]
        public void ShouldSetPropertiesWhenConstructedWithTokenTypeAndIndex()
        {
            var token = new Token<object>(TokenType.Word, 1);
            Assert.AreEqual(TokenType.Word, token.TokenType);
            Assert.AreEqual(1, token.Index);
            Assert.IsNull(token.TokenValue);
        }

        /// <summary>
        /// Tests that the token should return token value when set.
        /// </summary>
        [TestMethod]
        public void ShouldSetPropertiesWhenConstructedWithAllValues()
        {
            var token = new Token<object>(TokenType.Word, 1, "hello");
            Assert.AreEqual(TokenType.Word, token.TokenType);
            Assert.AreEqual(1, token.Index);
            Assert.AreEqual("hello", token.TokenValue);
        }

        /// <summary>
        /// Tests that the token should be equal when compared.
        /// </summary>
        [TestMethod]
        public void ShouldBeEqualWhenCompared()
        {
            var token1 = new Token<object>(TokenType.Word, 0, "hello");
            var token2 = new Token<object>(TokenType.Word, 0, "hello");
            Assert.IsTrue(token1 == token1);
            Assert.IsTrue(token1 == token2);
            Assert.IsTrue(token1 != null);
            Assert.IsTrue(null != token2);
            Assert.IsFalse(token1 != token2);
            Assert.IsTrue(token1.Equals(token2));
            Assert.IsTrue(token1.Equals((object)token2));
            Assert.IsTrue(token1.GetHashCode() == token2.GetHashCode());
        }

        /// <summary>
        /// Tests that the token should be not equal when compared and the toke type is different.
        /// </summary>
        [TestMethod]
        public void ShouldBeNotEqualWhenComparedAndTheTokeTypeIsDifferent()
        {
            var token1 = new Token<object>(TokenType.Word, 0, "hello");
            var token2 = new Token<object>(TokenType.WhiteSpace, 0, "hello");
            Assert.IsFalse(token1 == token2);
            Assert.IsTrue(token1 != token2);
            Assert.IsFalse(token1.Equals(token2));
            Assert.IsFalse(token1.Equals((object)token2));
            Assert.IsFalse(token1.GetHashCode() == token2.GetHashCode());
        }

        /// <summary>
        /// Tests that the token should be not equal when compared and the index is different.
        /// </summary>
        [TestMethod]
        public void ShouldBeNotEqualWhenComparedAndTheIndexIsDifferent()
        {
            var token1 = new Token<object>(TokenType.Word, 0, "hello");
            var token2 = new Token<object>(TokenType.Word, 1, "hello");
            Assert.IsFalse(token1 == token2);
            Assert.IsTrue(token1 != token2);
            Assert.IsFalse(token1.Equals(token2));
            Assert.IsFalse(token1.Equals((object)token2));
            Assert.IsFalse(token1.GetHashCode() == token2.GetHashCode());
        }

        /// <summary>
        /// Tests that the token should be not equal when compared and the value is different.
        /// </summary>
        [TestMethod]
        public void ShouldBeNotEqualWhenComparedAndTheValueIsDifferent()
        {
            var token1 = new Token<object>(TokenType.Word, 0, "hello");
            var token2 = new Token<object>(TokenType.Word, 0, "goodbye");
            Assert.IsFalse(token1 == token2);
            Assert.IsTrue(token1 != token2);
            Assert.IsFalse(token1.Equals(token2));
            Assert.IsFalse(token1.Equals((object)token2));
            Assert.IsFalse(token1.GetHashCode() == token2.GetHashCode());
        }

        /// <summary>
        /// Tests that the token should calaculate a different has code for an index of zero and null.
        /// </summary>
        [TestMethod]
        public void ShouldCalaculateADifferentHasCodeForAnIndexOfZeroAndNull()
        {
            var token1 = new Token<object>(TokenType.Word);
            var token2 = new Token<object>(TokenType.Word, 0);
            Assert.IsTrue(token1.GetHashCode() != token2.GetHashCode());
        }
    }
}
