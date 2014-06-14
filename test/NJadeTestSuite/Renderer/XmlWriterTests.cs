namespace NJadeTestSuite.Renderer
{
    using System;
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Render;

    /// <summary>
    /// Defines the XmlWriterTests class.
    /// </summary>
    [TestClass]
    public class XmlWriterTests
    {
        /// <summary>
        /// Tests that the XML writer should throw an argument null exception when the writer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowAnArgumentNullExceptionWhenTheWriterIsNull()
        {
            new XmlWriter(null);

            ////writer.WriteAttributeString();
            ////writer.WriteEndElement();
            ////writer.WriteStartElement();
        }

        /// <summary>
        /// Tests that the XML writer should set the default values when it is created.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheDefaultValuesWhenItIsCreated()
        {
            using (var textWriter = new StringWriter())
            using (var writer = new XmlWriter(textWriter))
            {
                Assert.IsFalse(writer.EnableAutoEncoding);
                Assert.AreEqual(0, writer.Indent);
                Assert.AreEqual(null, writer.IndentCharacters);
                Assert.AreEqual(null, writer.NewLineCharacters);
            }
        }

        /// <summary>
        /// Tests that the XML writer should flush the writer when disposed.
        /// </summary>
        [TestMethod]
        public void ShouldFlushTheWriterWhenDisposed()
        {
            var mockTextWriter = new Mock<TextWriter>();
            var writer = new XmlWriter(mockTextWriter.Object);
            writer.Dispose();
            mockTextWriter.Verify(t => t.Flush(), Times.Once);
        }

        /// <summary>
        /// Tests that the XML writer should encode an ampersand character.
        /// </summary>
        [TestMethod]
        public void ShouldEncodeAnAmpersandCharacter()
        {
            Assert.AreEqual("&amp;", XmlWriter.XmlEncode("&"));
        }

        /// <summary>
        /// Tests that the XML writer should encode a less than character.
        /// </summary>
        [TestMethod]
        public void ShouldEncodeALessThanCharacter()
        {
            Assert.AreEqual("&lt;", XmlWriter.XmlEncode("<"));
        }

        /// <summary>
        /// Tests that the XML writer should encode a grater than character.
        /// </summary>
        [TestMethod]
        public void ShouldEncodeAGraterThanCharacter()
        {
            Assert.AreEqual("&gt;", XmlWriter.XmlEncode(">"));
        }

        /// <summary>
        /// Tests that the XML writer should encode a double quote character.
        /// </summary>
        [TestMethod]
        public void ShouldEncodeADoubleQuoteCharacter()
        {
            Assert.AreEqual("&quot;", XmlWriter.XmlEncode("\""));
        }

        /// <summary>
        /// Tests that the XML writer should only encode ampersand character once.
        /// </summary>
        [TestMethod]
        public void ShouldOnlyEncodeAmpersandCharacterOnce()
        {
            Assert.AreEqual("&amp;&quot;", XmlWriter.XmlEncode("&\""));
        }

        /// <summary>
        /// Tests that the XML writer should return null when encoding null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenEncodingNull()
        {
            Assert.AreEqual(null, XmlWriter.XmlEncode(null));
        }

        /// <summary>
        /// Tests that the XML writer should retrun the same value when encoding whitespace or an empty string.
        /// </summary>
        [TestMethod]
        public void ShouldRetrunTheSameValueWhenEncodingWhitespaceOrAnEmptyString()
        {
            var emptyString = string.Empty;
            Assert.IsTrue(ReferenceEquals(emptyString, XmlWriter.XmlEncode(emptyString)));

            var whiteSpace = " \t\n\r";
            Assert.IsTrue(ReferenceEquals(whiteSpace, XmlWriter.XmlEncode(whiteSpace)));
        }

        /// <summary>
        /// Tests that the XML writer should write the new line characters when new line is called.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheNewLineCharactersWhenNewLineIsCalled()
        {
            var output = Exec(w =>
                {
                    w.NewLineCharacters = "<NL>";
                    w.WriteNewLine();
                });
            Assert.AreEqual("<NL>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write the indent characters when new line is called.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheIndentCharactersWhenNewLineIsCalled()
        {
            var output = Exec(w =>
                {
                    w.Indent = 1;
                    w.IndentCharacters = "<I>";
                    w.WriteNewLine();
                });
            Assert.AreEqual("<I>", output);
        }

        /// <summary>
        /// Tests that the XML writer should repeat the indent characters by the specified indent.
        /// </summary>
        [TestMethod]
        public void ShouldRepeatTheIndentCharactersByTheSpecifiedIndent()
        {
            var output = Exec(w =>
            {
                w.Indent = 3;
                w.IndentCharacters = "<I>";
                w.WriteNewLine();
            });
            Assert.AreEqual("<I><I><I>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write an XML document header with the supplied encoding.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAnXmlDocumentHeaderWithTheSuppliedEncoding()
        {
            var output = Exec(w => w.WriteStartDocument("enc"));
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"enc\"?>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line after writing the document header.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineAfterWritingTheDocumentHeader()
        {
            var output = Exec(
                w =>
                {
                    w.NewLineCharacters = "<NL>";
                    w.WriteStartDocument();
                    w.WriteElementString("tag", "text");
                });
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?><NL><tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a processing instruction.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAProcessingInstruction()
        {
            var output = Exec(w => w.WriteProcessingInstruction("instruction", "value"));
            Assert.AreEqual("<?instruction value?>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line after writing a processing instruction.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineAfterWritingAProcessingInstruction()
        {
            var output = Exec(
            w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteProcessingInstruction("instruction", "value");
                w.WriteElementString("tag", "text");
            });
            Assert.AreEqual("<?instruction value?><NL><tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a string without automatic encoding.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAStringWithoutAutoEncoding()
        {
            var output = Exec(w =>
                {
                    w.EnableAutoEncoding = false;
                    w.WriteString("A <string> & a character.");
                });

            Assert.AreEqual("A <string> & a character.", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a string with automatic encoding.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAStringWithAutomaticEncoding()
        {
            var output = Exec(w =>
            {
                w.EnableAutoEncoding = true;
                w.WriteString("A <string> & a character.");
            });

            Assert.AreEqual("A &lt;string&gt; &amp; a character.", output);
        }

        /// <summary>
        /// Tests that the XML writer should not write a new line when writing a string.
        /// </summary>
        [TestMethod]
        public void ShouldNotWriteANewLineWhenWritingAString()
        {
            var output = Exec(
                w =>
                {
                    w.NewLineCharacters = "<NL>";
                    w.WriteElementString("tag", "text");
                    w.WriteString("text");
                });
            Assert.AreEqual("<tag>text</tag>text", output);
        }

        /// <summary>
        /// Tests that the XML writer should allow writing of raw XML.
        /// </summary>
        [TestMethod]
        public void ShouldAllowWritingOfRawXml()
        {
            var output = Exec(w => w.WriteString("<tag>text</tag>"));
            Assert.AreEqual("<tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should allow writing of invalid XML.
        /// </summary>
        [TestMethod]
        public void ShouldAllowWritingOfInvalidXml()
        {
            var output = Exec(w => w.WriteString("<t/a<<g>te&&>xt</t</ag>"));
            Assert.AreEqual("<t/a<<g>te&&>xt</t</ag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write an element with inner text.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAnElementWithInnerText()
        {
            var output = Exec(w => w.WriteElementString("tag", "text"));
            Assert.AreEqual("<tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should automatically encode text when writing an element with inner text.
        /// </summary>
        [TestMethod]
        public void ShouldAutomaticallyEncodeTextWhenWritingAnElementWithInnerText()
        {
            var output = Exec(w =>
                {
                    w.EnableAutoEncoding = true;
                    w.WriteElementString("tag", "<");
                });
            Assert.AreEqual("<tag>&lt;</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should allow writing of an element with a namespace prefix.
        /// </summary>
        [TestMethod]
        public void ShouldAllowWritingOfAnElementWithANamespacePrefix()
        {
            var output = Exec(w => w.WriteElementString("name:tag", "text"));
            Assert.AreEqual("<name:tag>text</name:tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line between writing elements.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineBetweenWritingElements()
        {
            var output = Exec(
                w =>
                {
                    w.NewLineCharacters = "<NL>";
                    w.WriteElementString("tag", "text");
                    w.WriteElementString("tag", "text");
                });
            Assert.AreEqual("<tag>text</tag><NL><tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write an attribute and value.
        /// </summary>
        [TestMethod]
        public void ShouldWriteAnAttributeAndValue()
        {
            var output = Exec(w => w.WriteAttributeString("attribute", "value"));
            Assert.AreEqual(" attribute=\"value\"", output);
        }

        /// <summary>
        /// Tests that the XML writer should automatically encode the value when writing an attribute and value.
        /// </summary>
        [TestMethod]
        public void ShouldAutomaticallyEncodeTheValueWhenWritingAnAttributeAndValue()
        {
            var output = Exec(w =>
            {
                w.EnableAutoEncoding = true;
                w.WriteAttributeString("attribute", "&");
            });
            Assert.AreEqual(" attribute=\"&amp;\"", output);
        }

        /// <summary>
        /// Tests that the XML writer should allow writing of an attributes with a namespace prefix.
        /// </summary>
        [TestMethod]
        public void ShouldAllowWritingOfAnAttributesWithANamespacePrefix()
        {
            var output = Exec(w => w.WriteAttributeString("name:attribute", "value"));
            Assert.AreEqual(" name:attribute=\"value\"", output);
        }

        /// <summary>
        /// Tests that the XML writer should write the start of an element.
        /// </summary>
        [TestMethod]
        public void ShouldWriteTheStartOfAnElement()
        {
            var output = Exec(w => w.WriteStartElement("tag"));
            Assert.AreEqual("<tag", output);
        }

        /// <summary>
        /// Tests that the XML writer should automatically complete the opening element when writing a string.
        /// </summary>
        [TestMethod]
        public void ShouldAutomaticallyCompleteTheOpeningElementWhenWritingAString()
        {
            var output = Exec(w =>
                {
                    w.WriteStartElement("tag");
                    w.WriteString("text");
                });
            Assert.AreEqual("<tag>text", output);
        }

        /// <summary>
        /// Tests that the XML writer should not complete the opening element when writing attribute.
        /// </summary>
        [TestMethod]
        public void ShouldNotCompleteTheOpeningElementWhenWritingAttribute()
        {
            var output = Exec(w =>
            {
                w.WriteStartElement("tag");
                w.WriteAttributeString("attribute", "value");
            });
            Assert.AreEqual("<tag attribute=\"value\"", output);
        }

        /// <summary>
        /// Tests that the XML writer should not write a new line when adding text to an element.
        /// </summary>
        [TestMethod]
        public void ShouldNotWriteANewLineWhenAddingTextToAnElement()
        {
            var output = Exec(w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteStartElement("tag");
                w.WriteString("text");
            });
            Assert.AreEqual("<tag>text", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line when starting a child element.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineWhenStartingAChildElement()
        {
            var output = Exec(w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteStartElement("tag");
                w.WriteStartElement("tag");
            });
            Assert.AreEqual("<tag><NL><tag", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line when adding a child element with content.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineWhenAddingAChildElementWithContent()
        {
            var output = Exec(w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteStartElement("tag");
                w.WriteElementString("tag", "text");
            });
            Assert.AreEqual("<tag><NL><tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should close an element after it has been started.
        /// </summary>
        [TestMethod]
        public void ShouldCloseAnElementAfterItHasBeenStarted()
        {
            var output = Exec(w =>
            {
                w.WriteStartElement("tag");
                w.WriteString("text");
                w.WriteEndElement();
            });
            Assert.AreEqual("<tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should close an element with child elements.
        /// </summary>
        [TestMethod]
        public void ShouldCloseAnElementWithChildElements()
        {
            var output = Exec(w =>
            {
                w.WriteStartElement("tag");
                w.WriteElementString("tag", "text");
                w.WriteEndElement();
            });
            Assert.AreEqual("<tag><tag>text</tag></tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should not write a new line when ending an element with only text content.
        /// </summary>
        [TestMethod]
        public void ShouldNotWriteANewLineWhenEndingAnElementWithOnlyTextContent()
        {
            var output = Exec(w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteStartElement("tag");
                w.WriteString("text");
                w.WriteEndElement();
            });
            Assert.AreEqual("<tag>text</tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should write a new line when ending an element with child elements.
        /// </summary>
        [TestMethod]
        public void ShouldWriteANewLineWhenEndingAnElementWithChildElements()
        {
            var output = Exec(w =>
            {
                w.NewLineCharacters = "<NL>";
                w.WriteStartElement("tag");
                w.WriteElementString("tag", "text");
                w.WriteEndElement();
            });
            Assert.AreEqual("<tag><NL><tag>text</tag><NL></tag>", output);
        }

        /// <summary>
        /// Tests that the XML writer should create a self closing element when closing an element with no content.
        /// </summary>
        [TestMethod]
        public void ShouldCreateASelfClosingElementWhenClosingAnElementWithNoContent()
        {
            var output = Exec(w =>
            {
                w.WriteStartElement("tag");
                w.WriteEndElement();
            });
            Assert.AreEqual("<tag/>", output);
        }

        /// <summary>
        /// Executes the specified action on a new XmlWriter and return the result.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The text written.</returns>
        private static string Exec(Action<XmlWriter> action)
        {
            var sb = new StringBuilder();
            using (var textWriter = new StringWriter(sb))
            using (var writer = new XmlWriter(textWriter))
            {
                action(writer);
            }

            return sb.ToString();
        }
    }
}
