namespace NJade.Lexer.Matcher
{
    using Lexer.Tokenizer;

    /// <summary>
    /// Defines the IMatcher interface.
    /// </summary>
    /// <typeparam name="TTokenizer">The type of the tokenizer.</typeparam>
    /// <typeparam name="TToken">The type of the token.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public interface IMatcher<in TTokenizer, out TToken, TItem>
        where TTokenizer : ITokenizableStreamBase<TItem>
        where TToken : Token<TItem>
        where TItem : class
    {
        /// <summary>
        /// Determines whether the matched can match the current position of the specified tokenizer.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>true is successfully matched; otherwise, false.</returns>
        bool IsMatch(TTokenizer tokenizer);

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>
        /// A token if found; otherwise, null.
        /// </returns>
        TToken GetToken(TTokenizer tokenizer);
    }
}