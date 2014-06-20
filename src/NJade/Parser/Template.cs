namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Monads;

    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the Template class.
    /// </summary>
    internal class Template : ITemplate
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Template" /> class.
        /// </summary>
        /// <param name="docType">Type of the document.</param>
        /// <param name="elements">The elements.</param>
        public Template(DocType docType, List<JElement> elements)
        {
            elements.CheckNull("elements");

            this.DocType = docType;
            this.Elements = elements;
        }

        /// <summary>
        /// Gets the type of the document.
        /// </summary>
        public DocType DocType { get; private set; }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        public List<JElement> Elements { get; private set; }
    }
}