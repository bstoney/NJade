using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using NJade.Parser;
    using NJade.Parser.Elements;

    using NJadeTestSuite.TestHelper;

    [TestClass]
    public class JadeParserTests
    {
        /// <summary>
        /// Tests that the jade parser should return a new template when parsing a string.
        /// </summary>
        [TestMethod]
        public void ShouldReturnANewTemplateWhenParsingAString()
        {
            var jadeParser = new JadeParser();
            var template = jadeParser.Parse("a string");
            Assert.IsNotNull(template);
        }

        /// <summary>
        /// Tests that the jade parser should an argument null exception when parsing a string and the string is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: jade")]
        public void ShouldthrowAnArgumentNullExceptionWhenParsingAStringAndTheStringIsNull()
        {
            var jadeParser = new JadeParser();
            jadeParser.Parse(null);
        }

        /// <summary>
        /// Tests that the jade parser should return a template with no document type or elements when parsing an empty string.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATemplateWithNoDocTypeOrElementsWhenParsingAnEmptyString()
        {
            var jadeParser = new JadeParser();
            var template = jadeParser.Parse(string.Empty);

            Assert.IsNull(template.DocType);
            Assert.AreEqual(0, template.Elements.Count);
        }

        /// <summary>
        /// Tests that the jade parser should return a template with a document type when parsing a string with a document type.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATemplateWithADocTypeWhenParsingAStringWithADocType()
        {
            var jadeParser = new JadeParser();
            var template = jadeParser.Parse("doctype hello");

            Assert.IsNotNull(template.DocType);
            Assert.AreEqual(0, template.Elements.Count);
        }

        /// <summary>
        /// Tests that the jade parser should return a template with elements when parsing a string with elements.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATemplateWithElementsWhenParsingAStringWithElements()
        {
            var jadeParser = new JadeParser();
            var template = jadeParser.Parse("hello");

            Assert.IsNull(template.DocType);
            Assert.AreEqual(1, template.Elements.Count);
        }

        /// <summary>
        /// Tests that the jade parser should return a template with a document type and elements when parsing a string with a document type and elements.
        /// </summary>
        [TestMethod]
        public void ShouldReturnATemplateWithADocTypeAndElementsWhenParsingAStringWithADocTypeAndElements()
        {
            var jadeParser = new JadeParser();
            var template = jadeParser.Parse("doctype hello\nhello");

            Assert.IsNotNull(template.DocType);
            Assert.AreEqual(1, template.Elements.Count);
        }
    }
}
