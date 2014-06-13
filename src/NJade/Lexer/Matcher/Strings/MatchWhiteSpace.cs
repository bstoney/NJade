namespace NJade.Lexer.Matcher.Strings
{
    using System.Text;

    using Lexer.Tokenizer;
    using Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchWhiteSpace class.
    /// </summary>
    public class MatchWhiteSpace : StringMatcherBase
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>
        /// A token if the matcher matches successfully; otherwise, null.
        /// </returns>
        protected override StringToken GetTokenImpl(StringTokenizer tokenizer)
        {
            var location = tokenizer.GetCurrentLocation();

            bool foundWhiteSpace = false;
            var whitesSpace = new StringBuilder();
            while (!tokenizer.IsAtEnd() && string.IsNullOrWhiteSpace(tokenizer.Current))
            {
                foundWhiteSpace = true;

                whitesSpace.Append(tokenizer.Current);
                tokenizer.Consume();
            }

            if (foundWhiteSpace)
            {
                return location.CreateToken(TokenType.WhiteSpace, whitesSpace.ToString());
            }

            return null;
        }
    }
}