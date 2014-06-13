namespace NJade.Lexer.Matcher.Strings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;
    using System.Text;

    using Lexer.Tokenizer;
    using Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchWord class.
    /// </summary>
    public class MatchWord : StringMatcherBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchWord"/> class.
        /// </summary>
        /// <param name="specialCharacters">The special characters.</param>
        public MatchWord(List<StringMatcherBase> specialCharacters)
        {
            specialCharacters.CheckNull("specialCharacters");
            this.SpecialCharacters = specialCharacters;
        }

        /// <summary>
        /// Gets or sets the special characters.
        /// </summary>
        protected List<StringMatcherBase> SpecialCharacters { get; set; }

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
            bool foundWord = false;
            var word = new StringBuilder();
            while (!tokenizer.IsAtEnd() && !string.IsNullOrWhiteSpace(tokenizer.Current) && !this.SpecialCharacters.With(s => s.Any(c => c.IsMatch(tokenizer))))
            {
                foundWord = true;
                word.Append(tokenizer.Current);

                tokenizer.Consume();
            }

            return foundWord ? location.CreateToken(TokenType.Word, word.ToString()) : null;
        }
    }
}