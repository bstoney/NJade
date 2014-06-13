namespace NJade.Lexer.Matcher
{
    using Lexer.Tokenizer;

    /// <summary>
    /// Defines the MatcherBase class.
    /// </summary>
    /// <typeparam name="TTokenizer">The type of the tokenizer.</typeparam>
    /// <typeparam name="TToken">The type of the token.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public abstract class MatcherBase<TTokenizer, TToken, TItem> : IMatcher<TTokenizer, TToken, TItem>
        where TTokenizer : ITokenizableStreamBase<TItem>
        where TToken : Token<TItem>
        where TItem : class
    {
        /// <summary>
        /// Determines whether the specified tokenizer is match. This action does not consume the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>
        /// true if a token if found; otherwise, false.
        /// </returns>
        public bool IsMatch(TTokenizer tokenizer)
        {
            if (tokenizer.IsAtEnd())
            {
                return true;
            }

            tokenizer.TakeSnapshot();

            try
            {
                return this.GetTokenImpl(tokenizer) != null;
            }
            finally
            {
                tokenizer.RollbackSnapshot();
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>A token if matched; otherwise null.</returns>
        public TToken GetToken(TTokenizer tokenizer)
        {
            if (tokenizer.IsAtEnd())
            {
                return null;
            }

            tokenizer.TakeSnapshot();

            var token = this.GetTokenImpl(tokenizer);

            if (token == null)
            {
                tokenizer.RollbackSnapshot();
            }
            else
            {
                tokenizer.CommitSnapshot();
            }

            return token;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>
        /// A token if the matcher matches successfully; otherwise, null.
        /// </returns>
        protected abstract TToken GetTokenImpl(TTokenizer tokenizer);
    }
}