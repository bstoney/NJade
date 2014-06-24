namespace NJadeTestSuite.Parser.Parsers
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
    using NJade.Parser.Elements;
    using NJade.Parser.Parsers;

    using NJadeTestSuite.TestHelper;

    [TestClass]
    public class TagParserTests
    {
        /// <summary>
        /// Tests that the tag parser should return true when calling can parse and a dot is the next token.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenCallingCanParseAndADotIsTheNextToken()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(JadeTokenType.Dot, 1, 1, 1));

            var tagParser = new TagParser();
            Assert.IsTrue(tagParser.CanParse(tokens.Object));
        }

        /// <summary>
        /// Tests that the tag parser should return true when calling can parse and a hash is the next token.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenCallingCanParseAndAHashIsTheNextToken()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(JadeTokenType.Hash, 1, 1, 1));

            var tagParser = new TagParser();
            Assert.IsTrue(tagParser.CanParse(tokens.Object));
        }

        /// <summary>
        /// Tests that the tag parser should return true when calling can parse and a word is the next token.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenCallingCanParseAndAWordIsTheNextToken()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.Word, 1, 1, 1));

            var tagParser = new TagParser();
            Assert.IsTrue(tagParser.CanParse(tokens.Object));
        }

        /// <summary>
        /// Tests that the tag parser should return false when calling can parse andhe next token is not a dot, hash or word.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenCallingCanParseAndheNextTokenIsNotADotHashOrWord()
        {
            var tokens = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            tokens.Setup(t => t.Current).Returns(new StringToken(TokenType.QuotedString, 1, 1, 1));

            var tagParser = new TagParser();
            Assert.IsFalse(tagParser.CanParse(tokens.Object));
        }

        /// <summary>
        /// Tests that the tag parser should return a tag when calling parse.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATagWhenCallingParse()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var element = tagParser.Parse(tokens.Object);
            Assert.IsNotNull(element);
            Assert.IsInstanceOfType(element, typeof(JTag));
        }

        /// <summary>
        /// Tests that the tag parser should use the word as the tag when calling parse and the first token is a word.
        /// </summary>
        [TestMethod]
        public void ShouldUseTheWordAsTheTagWhenCallingParseAndTheFirstTokenIsAWord()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            Assert.AreEqual("hello", tag.Tag);
        }

        /// <summary>
        /// Tests that the tag parser should parse a class name when calling parse and a class name follows the tag.
        /// </summary>
        [TestMethod]
        public void ShouldParseAClassNameWhenCallingParseAndAClassNameFollowsTheTag()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Dot, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            CollectionAssert.AreEqual(new[] { "hello" }, tag.Classes);
        }

        /// <summary>
        /// Tests that the tag parser should parse multiple class names when calling parse and multiple class names follow the tag.
        /// </summary>
        [TestMethod]
        public void ShouldParseMultipleClassNamesWhenCallingParseAndMultipleClassNamesFollowTheTag()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Dot, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Dot, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            CollectionAssert.AreEqual(new[] { "hello", "hello" }, tag.Classes);
        }

        /// <summary>
        /// Tests that the tag parser should parse a class name when calling parse andd the first token is a dot followed by a class name.
        /// </summary>
        [TestMethod]
        public void ShouldParseAClassNameWhenCallingParseAnddTheFirstTokenIsADotFollowedByAWord()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(JadeTokenType.Dot, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            CollectionAssert.AreEqual(new[] { "hello" }, tag.Classes);
        }

        /// <summary>
        /// Tests that the tag parser should use div as the default tag when calling parse only a class name is provided.
        /// </summary>
        [TestMethod]
        public void ShouldUseDivAsTheDefaultTagWhenCallingParseOnlyAClassNameIsProvided()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(JadeTokenType.Dot, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            Assert.AreEqual("div", tag.Tag);
        }

        /// <summary>
        /// Tests that the tag parser should parse an identifier when calling parse and an identifier follows the tag.
        /// </summary>
        [TestMethod]
        public void ShouldParseAnIdWhenCallingParseAndAnIdFollowsTheTag()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Hash, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            Assert.AreEqual("hello", tag.Id);
        }

        /// <summary>
        /// Tests that the tag parser should parse an identifier when calling parse andd the first token is a hash followed by a word.
        /// </summary>
        [TestMethod]
        public void ShouldParseAnIdWhenCallingParseAnddTheFirstTokenIsAHashFollowedByAWord()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(JadeTokenType.Hash, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            Assert.AreEqual("hello", tag.Id);
        }

        /// <summary>
        /// Tests that the tag parser should use div as the default tag when calling parse only an identifier is provided.
        /// </summary>
        [TestMethod]
        public void ShouldUseDivAsTheDefaultTagWhenCallingParseOnlyAnIdIsProvided()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(JadeTokenType.Hash, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            var tag = tagParser.Parse(tokens.Object) as JTag;

            Assert.AreEqual("div", tag.Tag);
        }

        /// <summary>
        /// Tests that the tag parser should throw an invalid token exception when calling parse and multiple ids follow the tag.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(NJadeException), "Unexpected hash at line: 1, column: 1.")]
        public void ShouldThrowAnInvalidTokenExceptionWhenCallingParseAndMultipleIdsFollowTheTag()
        {
            var tokens = new Mock<TokenLine>((IEnumerable<StringToken>)new[]
                                                                           {
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Hash, 1, 1, 1, "hello"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello"),
                                                                               new StringToken(JadeTokenType.Hash, 1, 1, 1, "hash"),
                                                                               new StringToken(TokenType.Word, 1, 1, 1, "hello")
                                                                           });
            tokens.CallBase = true;

            var tagParser = new TagParser();
            tagParser.Parse(tokens.Object);
        }
    }
}
