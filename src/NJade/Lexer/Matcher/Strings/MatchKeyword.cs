namespace NJade.Lexer.Matcher.Strings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Monads;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the MatchKeyword class.
    /// </summary>
    public class MatchKeyword : StringMatcherBase
    {
        /// <summary>
        /// The keyword characters
        /// </summary>
        private readonly string[] keywordCharacters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchKeyword"/> class.
        /// </summary>
        /// <param name="tokenType">The type.</param>
        /// <param name="keyword">The keyword.</param>
        public MatchKeyword(TokenType tokenType, string keyword)
        {
            tokenType.CheckNull("tokenType");
            keyword.CheckNull("keyword");
            keyword.Check(
                s => !string.IsNullOrEmpty(s), s => new ArgumentException("The keyword cannot be an empty string."));
            this.Keyword = keyword;
            this.keywordCharacters = keyword.Select(c => c.ToString(CultureInfo.InvariantCulture)).ToArray();
            this.TokenType = tokenType;
            this.AllowAsSubString = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow matching as a sub-string.
        /// </summary>
        /// <remarks>
        /// If true then matching on { in a string like "{test" will match the first cahracter
        /// because it is not space delimited. If false it must be space or special character delimited
        /// </remarks>
        public bool AllowAsSubString { get; set; }

        /// <summary>
        /// Gets or sets the special characters.
        /// </summary>
        public List<MatchKeyword> SpecialCharacters { get; set; }

        /// <summary>
        /// Gets the keyword.
        /// </summary>
        protected string Keyword { get; private set; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        protected TokenType TokenType { get; private set; }

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

            foreach (var character in this.keywordCharacters)
            {
                if (tokenizer.Current == character)
                {
                    tokenizer.Consume();
                }
                else
                {
                    return null;
                }
            }

            bool found;

            if (!this.AllowAsSubString)
            {
                var next = tokenizer.Current;

                found = string.IsNullOrWhiteSpace(next) || this.SpecialCharacters.With(s => s.Any(character => character.IsMatch(tokenizer)));
            }
            else
            {
                found = true;
            }

            return found ? location.CreateToken(this.TokenType, this.Keyword) : null;
        }
    }
}