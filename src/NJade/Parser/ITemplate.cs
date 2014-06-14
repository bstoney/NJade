namespace NJade.Parser
{
    using System.Collections.Generic;

    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the ITemplate interface.
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        DocType DocType { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        List<JElement> Elements { get; set; }
    }
}