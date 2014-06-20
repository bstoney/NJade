using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NJadeTestSuite.Parser
{
    using System.Collections.Generic;

    using NJade.Parser;
    using NJade.Parser.Elements;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the TemplateTests class.
    /// </summary>
    [TestClass]
    public class TemplateTests
    {
        /// <summary>
        /// Tests that the template should set all properties when creating a template.
        /// </summary>
        [TestMethod]
        public void ShouldSetAllPropertiesWhenCreatingATemplate()
        {
            var docType = new DocType("hello");
            var elements = new List<JElement>();
            var template = new Template(docType, elements);

            Assert.AreEqual(docType, template.DocType);
            Assert.AreEqual(elements, template.Elements);
        }

        /// <summary>
        /// Tests that the template should throw an argument null exception when when creating a template and the elements are null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: elements")]
        public void ShouldThrowAnArgumentNullExceptionWhenWhenCreatingATemplateAndTheElementsAreNull()
        {
            var docType = new DocType("hello");
            new Template(docType, null);
        }

        /// <summary>
        /// Tests that the template should allow null as the document type when creating a template.
        /// </summary>
        [TestMethod]
        public void ShouldAllowNullAsTheDocTypeWhenCreatingATemplate()
        {
            var elements = new List<JElement>();
            new Template(null, elements);
        }
    }
}
