namespace NJade.Lexer.Matcher.Strings
{
    using System;
    using System.Monads;
    using System.Text;

    using Lexer.Tokenizer;
    using Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchString class.
    /// </summary>
    public class MatchString : StringMatcherBase
    {
        /// <summary>
        /// The quote.
        /// </summary>
        public const string DoubleQuote = "\"";

        /// <summary>
        /// The tic.
        /// </summary>
        public const string SingleQuote = "'";

        /// <summary>
        /// Initialises a new instance of the <see cref="MatchString" /> class.
        /// </summary>
        /// <param name="delimiter">The delimiter.</param>
        public MatchString(string delimiter)
        {
            delimiter.CheckNull("delimiter");
            delimiter.Check(
                s => !string.IsNullOrEmpty(s), s => new ArgumentException("The delimiter cannot be an empty string."));
            this.StringDelimiter = delimiter;
        }

        /// <summary>
        /// Gets or sets the string delimiter.
        /// </summary>
        protected string StringDelimiter { get; private set; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>
        /// A token if the matcher matches successfully; otherwise, null.
        /// </returns>
        protected override StringToken GetTokenImpl(StringTokenizer tokenizer)
        {
            if (tokenizer.Current == this.StringDelimiter)
            {
                var location = tokenizer.GetCurrentLocation();
                var str = new StringBuilder();
                tokenizer.Consume();

                while (!tokenizer.IsAtEnd() && tokenizer.Current != this.StringDelimiter)
                {
                    str.Append(tokenizer.Current);
                    tokenizer.Consume();
                }

                if (tokenizer.Current == this.StringDelimiter)
                {
                    tokenizer.Consume();

                    return location.CreateToken(TokenType.QuotedString, str.ToString());
                }
            }

            return null;
        }
    }
}