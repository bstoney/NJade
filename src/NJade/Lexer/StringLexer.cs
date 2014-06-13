namespace NJade.Lexer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    using Lexer.Matcher;
    using Lexer.Matcher.Strings;
    using Lexer.Tokenizer;
    using Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the Lexer class.
    /// </summary>
    public class StringLexer
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="StringLexer" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="matchers">The matchers.</param>
        public StringLexer(string source, IEnumerable<StringMatcherBase> matchers)
        {
            source.CheckNull("source");
            matchers.CheckNull("matchers");
            this.Tokenizer = new StringTokenizer(source);
            this.Matchers = matchers.ToList();
        }

        /// <summary>
        /// Gets the tokenizer.
        /// </summary>
        protected StringTokenizer Tokenizer { get; private set; }

        /// <summary>
        /// Gets the matchers.
        /// </summary>
        protected List<StringMatcherBase> Matchers { get; private set; }

        /// <summary>
        /// Tokenizes this instance.
        /// </summary>
        /// <returns>A list of tokens</returns>
        public virtual IEnumerable<StringToken> Tokenize()
        {
            var current = this.Next();

            while (current.With(c => c.TokenType != TokenType.Eof))
            {
                yield return current;

                current = this.Next();
            }
        }

        /// <summary>
        /// Initializes the match list.
        /// </summary>
        /// <summary>
        /// Nexts this instance.
        /// </summary>
        /// <returns>Gets the next token.</returns>
        private StringToken Next()
        {
            var location = this.Tokenizer.GetCurrentLocation();
            if (this.Tokenizer.IsAtEnd())
            {
                return location.CreateToken(TokenType.Eof);
            }

            return this.Matchers.With(ms => ms.Select(m => m.GetToken(this.Tokenizer)).FirstOrDefault(t => t != null));
        }
    }
}
