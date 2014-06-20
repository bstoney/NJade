namespace NJade.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the TokenLine class.
    /// </summary>
    internal class TokenLine : TokenStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenLine" /> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public TokenLine(IEnumerable<StringToken> tokens)
            : this(tokens, Enumerable.Empty<TokenLine>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenLine" /> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="children">The children.</param>
        public TokenLine(IEnumerable<StringToken> tokens, IEnumerable<TokenLine> children)
            : base(tokens)
        {
            children.CheckNull("children");
            this.Children = new TokenLineStream(children);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public TokenLineStream Children { get; private set; }
    }
}