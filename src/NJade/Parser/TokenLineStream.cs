namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the TokenLineStream class.
    /// </summary>
    internal class TokenLineStream : TokenizableStreamBase<TokenLine>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenLineStream" /> class.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public TokenLineStream(IEnumerable<TokenLine> lines)
            : base((lines ?? Enumerable.Empty<TokenLine>()).ToList)
        {
            lines.CheckNull("lines");
        }
    }
}