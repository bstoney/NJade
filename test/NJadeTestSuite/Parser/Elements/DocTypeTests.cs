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

        /////// <summary>
        /////// Tests that the document type should throw an argument null exception when creating a document type and the type is null.
        /////// </summary>
        ////[TestMethod]
        ////[ExpectedExceptionMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: type")]
        ////public void ShouldThrowAnArgumentNullExceptionWhenCreatingADocTypeAndTheTypeIsNull()
        ////{
        ////    new DocType(null);
        ////}

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
    }
}
