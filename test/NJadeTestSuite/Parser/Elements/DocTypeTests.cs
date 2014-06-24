namespace NJadeTestSuite.Parser.Elements
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Parser.Elements;
    using NJade.Render;

    using NJadeTestSuite.TestHelper;

    /// <summary>
    /// Defines the DocTypeTests class.
    /// </summary>
    [TestClass]
    public class DocTypeTests
    {
        /// <summary>
        /// Tests that the document type should set the type when creating a document type.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheTypeWhenCreatingADocType()
        {
            new DocType("hello");
        }

        /// <summary>
        /// Tests that the document type should throw an argument null exception when creating a document type and the type is null.
        /// </summary>
        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: type")]
        public void ShouldThrowAnArgumentNullExceptionWhenCreatingADocTypeAndTheTypeIsNull()
        {
            new DocType(null);
        }

        /// <summary>
        /// Tests that the document type should return the type when calling to string on a document type.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTheTypeWhenCallingToStringOnADocType()
        {
            var docType = new DocType("hello");
            Assert.AreEqual("hello", docType.ToString());
        }

        /// <summary>
        /// Tests that the document type should write the document type when calling render and the document type is not a know value.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDocTypeWhenCallingRenderAndTheDocTypeIsNotAKnowValue()
        {
            var docType = new DocType("hello");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString("<!DOCTYPE hello>"));
        }

        /// <summary>
        /// Tests that the document type should write the XML preprocessing instruction when calling render and the document type is XML.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheXmlPreprocessingInstructionWhenCallingRenderAndTheDocTypeIsXml()
        {
            var docType = new DocType("xml");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is transitional.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsTransitional()
        {
            var docType = new DocType("transitional");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml1-transitional.dtd"))));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is strict.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsStrict()
        {
            var docType = new DocType("strict");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml1-strict.dtd"))));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is frameset.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsFrameset()
        {
            var docType = new DocType("frameset");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml1-frameset.dtd"))));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is 1.1.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsOnePointOne()
        {
            var docType = new DocType("1.1");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml11.dtd"))));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is basic.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsBasic()
        {
            var docType = new DocType("basic");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml-basic11.dtd"))));
        }

        /// <summary>
        /// Tests that the document type should write the doctype when calling render and the document type is mobile.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheDoctypeWhenCallingRenderAndTheDocTypeIsMobile()
        {
            var docType = new DocType("mobile");
            var xmlWriter = Mock.Of<IXmlWriter>();
            docType.Render(xmlWriter);

            Mock.Get(xmlWriter).Verify(w => w.WriteString(It.Is<string>(s => s.Contains("xhtml-mobile12.dtd"))));
        }
    }
}
