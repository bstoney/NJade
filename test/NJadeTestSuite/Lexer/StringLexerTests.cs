namespace NJadeTestSuite.Lexer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer;
    using NJade.Lexer.Matcher.Strings;

    /// <summary>
    /// Defines the StringLexerTests class.
    /// </summary>
    [TestClass]
    public class StringLexerTests
    {
        /// <summary>
        /// Tests that the string lexer should throw an argument null exception when the source is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenTheSourceIsNull()
        {
            new StringLexer(null, new List<StringMatcherBase>());
        }

        /// <summary>
        /// Tests that the string lexer should throw an argument null exception when the matchers is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenTheMatchersIsNull()
        {
            new StringLexer(string.Empty, null);
        }

        /// <summary>
        /// Tests that the string lexer should return a list of tokens from the source.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAListOfTokensFromTheSource()
        {
            var stringLexer = new StringLexer("hello world", new List<StringMatcherBase> { new MatchWhiteSpace(), new MatchWord(new List<StringMatcherBase>()) });

            var tokens = stringLexer.Tokenize().ToList();

            Assert.AreEqual(3, tokens.Count());
            Assert.AreEqual("Word - hello", tokens.ElementAt(0).ToString());
            Assert.AreEqual("WhiteSpace -  ", tokens.ElementAt(1).ToString());
            Assert.AreEqual("Word - world", tokens.ElementAt(2).ToString());
        }

        /// <summary>
        /// Tests that the string lexer should return no tokens when the source is empty.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNoTokensWhenTheSourceIsEmpty()
        {
            var stringLexer = new StringLexer(string.Empty, new List<StringMatcherBase>());

            var tokens = stringLexer.Tokenize();

            Assert.AreEqual(0, tokens.Count());
        }

        /// <summary>
        /// Tests that the string lexer should return no tokens when no matchers are defined.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNoTokensWhenNoMatchersAreDefined()
        {
            var stringLexer = new StringLexer("hello world", new List<StringMatcherBase>());

            var tokens = stringLexer.Tokenize();

            Assert.AreEqual(0, tokens.Count());
        }

        /// <summary>
        /// Tests that the string lexer should stop returning tokens when an unknown token is encountered.
        /// </summary>
        [TestMethod]
        public void ShouldStopReturningTokensWhenAnUnknownTokenIsEncountered()
        {
            var stringLexer = new StringLexer("hello world", new List<StringMatcherBase> { new MatchWord(new List<StringMatcherBase>()) });

            var tokens = stringLexer.Tokenize();

            Assert.AreEqual(1, tokens.Count());
        }
    }
}
