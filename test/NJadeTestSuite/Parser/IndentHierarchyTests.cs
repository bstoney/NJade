using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the IndentHierarchyTests class.
    /// </summary>
    [TestClass]
    public class IndentHierarchyTests
    {
        /// <summary>
        /// Tests that the indent hierarchy should set all properties when creating indent hierarchy.
        /// </summary>
        [TestMethod]
        public void ShouldSetAllPropertiesWhenCreatingIndentHierarchy()
        {
            var line = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            var children = new IndentHierarchy[] { };
            var indentHierarchy = new IndentHierarchy(line.Object, children);

            Assert.AreEqual(line.Object, indentHierarchy.Line);
            CollectionAssert.AreEqual(children, indentHierarchy.Children);
        }

        /// <summary>
        /// Tests that the indent hierarchy should set all properties when creating indent hierarchy and only the line is supplied.
        /// </summary>
        [TestMethod]
        public void ShouldSetAllPropertiesWhenCreatingIndentHierarchyAndOnlyTheLineIsSupplied()
        {
            var line = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            var indentHierarchy = new IndentHierarchy(line.Object);

            Assert.AreEqual(line.Object, indentHierarchy.Line);
            CollectionAssert.AreEqual(new IndentHierarchy[] { }, indentHierarchy.Children);
        }

        /// <summary>
        /// Tests that the indent hierarchy should create a new array of the children when creating indent hierarchy.
        /// </summary>
        [TestMethod]
        public void ShouldCreateANewArrayOfTheChildrenWhenCreatingIndentHierarchy()
        {
            var line = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            var children = new IndentHierarchy[] { };
            var indentHierarchy = new IndentHierarchy(line.Object, children);

            Assert.AreNotEqual(children, indentHierarchy.Children);
        }

        /// <summary>
        /// Tests that the indent hierarchy should throw an argument null exception when creating indent hierarchy and the line is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: line")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingIndentHierarchyAndTheLineIsNull()
        {
            var children = new IndentHierarchy[] { };
            new IndentHierarchy(null, children);
        }

        /// <summary>
        /// Tests that the indent hierarchy should throw an argument null exception when creating indent hierarchy and only the line is supplied and the line is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: line")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingIndentHierarchyAndOnlyTheLineIsSuppliedAndTheLineIsNull()
        {
            new IndentHierarchy(null);
        }

        /// <summary>
        /// Tests that the indent hierarchy should throw an argument null exception when creating indent hierarchy and the children is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: children")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingIndentHierarchyAndTheChildrenIsNull()
        {
            var line = new Mock<TokenStream>(Enumerable.Empty<StringToken>());
            new IndentHierarchy(line.Object, null);
        }
    }
}
