namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the Template class.
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Template" /> class.
        /// </summary>
        /// <param name="docType">Type of the document.</param>
        /// <param name="elements">The elements.</param>
        private Template(DocType docType, List<JElement> elements)
        {
            this.DocType = docType;
            this.Elements = elements;
        }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public DocType DocType { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public List<JElement> Elements { get; set; }

        /// <summary>
        /// Produces the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A new template.</returns>
        public static Template Produce(TokenStream tokens)
        {
            DocType docType = null;
            if (tokens.Is(JadeTokenType.Doctype))
            {
                tokens.Consume();
                tokens.ConsumeAny(TokenType.WhiteSpace);
                docType = new DocType(tokens.Get(TokenType.Word));
                tokens.GetLine();
            }

            return new Template(docType, tokens.GetItems(null).ToList());
        }
    }
}