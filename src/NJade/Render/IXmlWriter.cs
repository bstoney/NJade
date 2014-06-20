namespace NJade.Render
{
    using System;

    internal interface IXmlWriter : IDisposable
    {
        /// <summary>
        /// Writes the NewLineCharacters and the Indent multiple of the IndentCharacters.
        /// </summary>
        void WriteNewLine();

        /// <summary>
        /// Writes a new element with the supplied name.
        /// </summary>
        /// <param name="name">The name.</param>
        void WriteStartElement(string name);

        /// <summary>
        /// Writes a complete element start and end tag pair with the supplied name and inner text.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void WriteElementString(string name, string value);

        /// <summary>
        /// Writes the end tag or closes the current element. 
        /// </summary>
        void WriteEndElement();

        /// <summary>
        /// Writes a complete attribute with the supplies name and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void WriteAttributeString(string name, string value);

        /// <summary>
        /// Writes a literal string.
        /// </summary>
        /// <param name="value">The value.</param>
        void WriteString(string value);
    }
}