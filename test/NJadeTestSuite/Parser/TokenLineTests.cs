using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TokenLineTests class.
    /// </summary>
    [TestClass]
    public class TokenLineTests
    {
        /// <summary>
        /// Tests that the token line should set all properties when creating a token line.
        /// </summary>
        [TestMethod]
        public void ShouldSetAllPropertiesWhenCreatingATokenLine()
        {
            var line = new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") };
            var children = new TokenLine[] { };
            var tokenLine = new TokenLine(line, children);

            CollectionAssert.AreEqual(line, tokenLine.GetAll());
            CollectionAssert.AreEqual(children, tokenLine.Children.GetAll());
        }

        /// <summary>
        /// Tests that the token line should set all properties when creating a token line and only the line is supplied.
        /// </summary>
        [TestMethod]
        public void ShouldSetAllPropertiesWhenCreatingATokenLineAndOnlyTheLineIsSupplied()
        {
            var line = new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") };
            var tokenLine = new TokenLine(line);

            CollectionAssert.AreEqual(line, tokenLine.GetAll());
            CollectionAssert.AreEqual(new TokenLine[] { }, tokenLine.Children.GetAll());
        }

        /// <summary>
        /// Tests that the token line should throw an argument null exception when creating a token line and the children is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: children")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingATokenLineAndTheChildrenIsNull()
        {
            var line = new[] { new StringToken(TokenType.Word, 1, 1, 1, "hello") };
            new TokenLine(line, null);
        }
    }
}
