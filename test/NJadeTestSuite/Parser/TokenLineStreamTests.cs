using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TokenLineStreamTests class.
    /// </summary>
    [TestClass]
    public class TokenLineStreamTests
    {
        /// <summary>
        /// Tests that the token line stream should set the items when creating a token line stream.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheItemsWhenCreatingATokenLineStream()
        {
            var lines = new[] { new TokenLine(new StringToken[] { }) };
            var tokenStream = new TokenLineStream(lines);

            CollectionAssert.AreEqual(lines, tokenStream.GetAll());
        }

        /// <summary>
        /// Tests that the token line stream should throw an argument null exception when creating a token line stream and the lines are null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: lines")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingATokenLineStreamAndTheLinesAreNull()
        {
            new TokenLineStream(null);
        }
    }
}
