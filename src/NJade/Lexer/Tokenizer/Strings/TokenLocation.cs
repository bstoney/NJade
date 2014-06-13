namespace NJade.Lexer.Tokenizer.Strings
{
    /// <summary>
    /// Defines the TokenLocation class.
    /// </summary>
    public class TokenLocation : ITokenLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenLocation"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="line">The line.</param>
        /// <param name="column">The column.</param>
        public TokenLocation(int index, int line, int column)
        {
            this.Index = index;
            this.Line = line;
            this.Column = column;
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the line.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>A new token.</returns>
        public StringToken CreateToken(TokenType tokenType)
        {
            return new StringToken(tokenType, this.Index, this.Line, this.Column);
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="value">The value.</param>
        /// <returns>A new token.</returns>
        public StringToken CreateToken(TokenType tokenType, string value)
        {
            return new StringToken(tokenType, this.Index, this.Line, this.Column, value);
        }
    }
}