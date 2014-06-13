namespace NJade.Lexer.Tokenizer.Strings
{
    /// <summary>
    /// Defines the ITokenLocation interface.
    /// </summary>
    public interface ITokenLocation
    {
        /// <summary>
        /// Gets the index.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the line.
        /// </summary>
        int Line { get; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        int Column { get; }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>A new token.</returns>
        StringToken CreateToken(TokenType tokenType);

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="value">The value.</param>
        /// <returns>A new token.</returns>
        StringToken CreateToken(TokenType tokenType, string value);
    }
}