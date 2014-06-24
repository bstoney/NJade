namespace NJade.Parser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the ElementParser class.
    /// </summary>
    internal class ElementParser
    {
        /// <summary>
        /// The parsers
        /// </summary>
        private readonly List<IParser> parsers;

        /// <summary>
        /// Initialises a new instance of the <see cref="ElementParser"/> class.
        /// </summary>
        public ElementParser()
        {
            this.parsers = new List<IParser>
                               {
                                   new TagParser()
                               };
        }

        /// <summary>
        /// Parses the next element.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <returns>An element if one is found; otherwise, null.</returns>
        public JElement ParseNextElement(TokenLineStream lines)
        {
            lines.CheckNull("lines");
            lines.Check(
                l => !l.IsAtEnd(),
                l => new InvalidOperationException("Next element cannot be parsed when there are no lines available."));

            var tokens = lines.Current;
            lines.Consume();
            tokens.ConsumeAny(TokenType.WhiteSpace);

            try
            {
                return this.parsers.FirstOrDefault(parser => parser.CanParse(tokens)).With(p => p.Parse(tokens));
            }
            finally
            {
                tokens.ConsumeAny(JadeTokenType.NewLine);
                if (!tokens.IsAtEnd())
                {
                    tokens.RaiseUnexpectedToken();
                }
            }
        }
    }
}
