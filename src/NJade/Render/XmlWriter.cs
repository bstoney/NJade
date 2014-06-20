// ----------------------------------------------------------------------
// <copyright file="XmlWriter.cs" company="Plan B">
//   Copyright (c) Plan B.
// </copyright>
// ------------------------------------------------------------------------

namespace NJade.Render
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Monads;
    using System.Text;

    /// <summary>
    /// Custom XmlWriter to allow writing to document fragments to different files.
    /// </summary>
    internal sealed class XmlWriter : IXmlWriter
    {
        /// <summary>
        /// A map of invalid XML characters to their replacements.
        /// </summary>
        private static readonly Dictionary<string, string> InvalidCharacters = new Dictionary<string, string> { { "<", "&lt;" }, { ">", "&gt;" }, { "\"", "&quot;" } };

        /// <summary>
        /// The writer.
        /// </summary>
        private readonly TextWriter writer;

        /// <summary>
        /// A stack of the current element hierarchy.
        /// </summary>
        private readonly Stack<string> elementStack;

        /// <summary>
        /// Indicates whether the current writing state is within the begin tag of an element.
        /// </summary>
        private bool currentlyWritingElement;

        /// <summary>
        /// Indicates that a new line should be written before the next write command.
        /// </summary>
        private bool needsNewLine;

        /// <summary>
        /// Initialises a new instance of the XmlWriter class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public XmlWriter(TextWriter writer)
        {
            writer.CheckNull("writer");

            this.writer = writer;
            this.elementStack = new Stack<string>();
        }

        /// <summary>
        /// Gets or sets the current writer indent level.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Gets or sets the string used to indent.
        /// </summary>
        public string IndentCharacters { get; set; }

        /// <summary>
        /// Gets or sets the string used for a new line.
        /// </summary>
        public string NewLineCharacters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether auto encoding is enabled.
        /// </summary>
        public bool EnableAutoEncoding { get; set; }

        /// <summary>
        /// Encodes a string for use in xml.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The encoded text.</returns>
        public static string XmlEncode(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var sb = new StringBuilder(text);
                sb.Replace("&", "&amp;");

                foreach (var item in InvalidCharacters)
                {
                    sb.Replace(item.Key, item.Value);
                }

                return sb.ToString();
            }

            return text;
        }

        /// <summary>
        /// Writes the NewLineCharacters and the Indent multiple of the IndentCharacters.
        /// </summary>
        public void WriteNewLine()
        {
            this.Write(this.NewLineCharacters);
            for (int i = 0; i < this.Indent; i++)
            {
                this.Write(this.IndentCharacters);
            }

            this.needsNewLine = false;
        }

        /// <summary>
        /// Writes the standard xml document header.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public void WriteStartDocument(string encoding = "utf-8")
        {
            this.NewLine();
            this.Write("<?xml version=\"1.0\" encoding=\"", encoding, "\"?>");
            this.needsNewLine = true;
        }

        /// <summary>
        /// Writes a processing instruction to the document.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void WriteProcessingInstruction(string name, string value)
        {
            this.NewLine();
            this.Write("<?", name, " ", value, "?>");
            this.needsNewLine = true;
        }

        /// <summary>
        /// Writes a new element with the supplied name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void WriteStartElement(string name)
        {
            this.EnsureElementStartEnd();
            this.NewLine();
            this.Write("<", name);
            this.elementStack.Push(name);
            this.currentlyWritingElement = true;
            this.Indent++;
        }

        /// <summary>
        /// Writes a complete element start and end tag pair with the supplied name and inner text.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void WriteElementString(string name, string value)
        {
            this.WriteStartElement(name);
            this.EnsureElementStartEnd();
            this.needsNewLine = false;
            this.WriteString(value);
            this.needsNewLine = false;
            this.WriteEndElement();
        }

        /// <summary>
        /// Writes the end tag or closes the current element. 
        /// </summary>
        public void WriteEndElement()
        {
            this.Indent--;
            string name = this.elementStack.Pop();
            if (this.currentlyWritingElement)
            {
                this.Write("/>");
            }
            else
            {
                this.NewLine();
                this.Write("</", name, ">");
            }

            this.currentlyWritingElement = false;
            this.needsNewLine = true;
        }

        /// <summary>
        /// Writes a complete attribute with the supplies name and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void WriteAttributeString(string name, string value)
        {
            this.Write(" ", name, "=\"", this.EnableAutoEncoding ? XmlEncode(value) : value, "\"");
        }

        /// <summary>
        /// Writes a literal string.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteString(string value)
        {
            this.EnsureElementStartEnd();
            this.Write(this.EnableAutoEncoding ? XmlEncode(value) : value);

            // Strings must not have additional characters on either side.
            this.needsNewLine = false;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.writer.Flush();
        }

        /// <summary>
        /// Writes a new line.
        /// </summary>
        private void NewLine()
        {
            if (this.needsNewLine)
            {
                this.WriteNewLine();
            }
        }

        /// <summary>
        /// Writes the specified arguments as raw text to the document.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private void Write(params string[] args)
        {
            args.ToList().ForEach(arg => this.writer.Write(arg));
        }

        /// <summary>
        /// Ensures the element start tag is closed.
        /// </summary>
        private void EnsureElementStartEnd()
        {
            if (this.currentlyWritingElement)
            {
                this.Write(">");
                this.currentlyWritingElement = false;
                this.needsNewLine = true;
            }
        }
    }
}