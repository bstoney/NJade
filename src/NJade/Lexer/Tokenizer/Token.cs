namespace NJade.Lexer.Tokenizer
{
    /// <summary>
    /// Defines the Token class.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Token<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token{TValue}"/> class. 
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        internal Token(TokenType tokenType)
        {
            this.TokenType = tokenType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token{TValue}" /> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        internal Token(TokenType tokenType, int index)
            : this(tokenType)
        {
            this.TokenType = tokenType;
            this.Index = index;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token{TValue}" /> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal Token(TokenType tokenType, int index, TValue value)
            : this(tokenType, index)
        {
            this.TokenValue = value;
        }

        /// <summary>
        /// Gets the token value.
        /// </summary>
        public TValue TokenValue { get; private set; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public TokenType TokenType { get; private set; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.TokenType, this.TokenValue);
        }
    }
}