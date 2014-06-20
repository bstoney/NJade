﻿namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Monads;

    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the Parser class.
    /// </summary>
    public class JadeParser
    {
        /// <summary>
        /// Gets the jade template.
        /// </summary>
        /// <returns>A new jade template.</returns>
        public ITemplate Parse(string jade)
        {
            jade.CheckNull("jade");

            var lexer = new JadeLexer(jade);
            var rawTokens = new TokenStream(lexer.Tokenize());
            var lines = rawTokens.GetLines();

            DocType docType = null;
            var elements = new List<JElement>();
            if (!lines.IsAtEnd())
            {
                var firstLine = lines.Current;
                if (firstLine.Is(JadeTokenType.Doctype))
                {
                    firstLine.Consume();
                    firstLine.GetAny(TokenType.WhiteSpace);
                    docType = new DocType(firstLine.GetAny(TokenType.Word));
                    lines.Consume();
                }

                while (!lines.IsAtEnd())
                {
                    this.ParseNextElement(lines).Do(elements.Add);
                }
            }

            return new Template(docType, elements);
        }

        /// <summary>
        /// Parses the next element.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <returns>An element if one is found; otherwise, null.</returns>
        private JElement ParseNextElement(TokenLineStream lines)
        {
            TokenLine tokens = lines.Current;
            tokens.GetAny(TokenType.WhiteSpace);
            if (!tokens.Is(JadeTokenType.NewLine))
            {
                tokens.RaiseUnexpectedToken();
            }

            lines.Consume();
            return null;
        }
    }
}
