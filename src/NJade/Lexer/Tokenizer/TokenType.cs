namespace NJade.Lexer.Tokenizer
{
    /// <summary>
    /// Defines the TokenType class.
    /// </summary>
    public class TokenType
    {
        /// <summary>
        /// The EOF.
        /// </summary>
        public static readonly TokenType Eof = new TokenType("EOF");

        /// <summary>
        /// The white space.
        /// </summary>
        public static readonly TokenType WhiteSpace = new TokenType("WhiteSpace");

        /// <summary>
        /// The word.
        /// </summary>
        public static readonly TokenType Word = new TokenType("Word");

        /// <summary>
        /// The quoted string
        /// </summary>
        public static readonly TokenType QuotedString = new TokenType("QuotedString");

        /// <summary>
        /// The description
        /// </summary>
        private readonly string description;

        /// <summary>
        /// Initialises a new instance of the <see cref="TokenType" /> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public TokenType(string description)
        {
            this.description = description;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return !string.IsNullOrEmpty(this.description) ? this.description : base.ToString();
        }
    }
}