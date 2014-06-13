namespace NJade.Lexer.Tokenizer.Strings
{
    /// <summary>
    /// Defines the StringToken class.
    /// </summary>
    public class StringToken : Token<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringToken" /> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        /// <param name="line">The line.</param>
        /// <param name="column">The column.</param>
        internal StringToken(TokenType tokenType, int index, int line, int column)
            : base(tokenType, index)
        {
            this.Line = line;
            this.Column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToken"/> class. 
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        /// <param name="line">The line.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        internal StringToken(TokenType tokenType, int index, int line, int column, string value)
            : base(tokenType, index, value)
        {
            this.Line = line;
            this.Column = column;
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column { get; private set; }
    }
}
