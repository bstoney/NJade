namespace NJade.Lexer
{
    using System.Collections.Generic;
    using System.Linq;

    using NJade.Lexer.Matcher.Strings;

    /// <summary>
    /// Defines the Lexer class.
    /// </summary>
    public class JadeLexer : StringLexer
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="JadeLexer"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public JadeLexer(string source)
            : base(Normalize(source), InitializeMatchers())
        {
        }

        /// <summary>
        /// Normalizes the specified string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A string.</returns>
        private static string Normalize(string source)
        {
            return source.Replace("\r", string.Empty);
        }

        /// <summary>
        /// Initializes the matchers.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<StringMatcherBase> InitializeMatchers()
        {
            // The order here matters because it defines token precedence.
            var matchers = new List<StringMatcherBase>();

            var specialCharacters = new List<StringMatcherBase>
                                        {
                                            new MatchKeyword(JadeTokenType.OpenBracket, "{"),
                                            new MatchKeyword(JadeTokenType.CloseBracket, "}"),
                                            new MatchKeyword(JadeTokenType.Plus, "+"),
                                            new MatchKeyword(JadeTokenType.InlineValue, "= "),
                                            new MatchKeyword(JadeTokenType.Equals, "="),
                                            new MatchKeyword(JadeTokenType.HashTag, "#"),
                                            new MatchKeyword(JadeTokenType.Comma, ","),
                                            new MatchKeyword(JadeTokenType.OpenParenth, "("),
                                            new MatchKeyword(JadeTokenType.CloseParenth, ")"),
                                            new MatchKeyword(JadeTokenType.Comment, "//"),
                                            new MatchKeyword(JadeTokenType.JadeComment, "//-"),
                                            new MatchKeyword(JadeTokenType.Bang, "!"),
                                            new MatchKeyword(JadeTokenType.Pipe, "|"),
                                            new MatchKeyword(JadeTokenType.Colon, ":"),
                                            new MatchKeyword(JadeTokenType.Block, ".\n"),
                                            new MatchKeyword(JadeTokenType.Dot, "."),
                                            new MatchKeyword(JadeTokenType.NewLine, "\n"),
                                        };
            var keywordmatchers = new List<StringMatcherBase> 
                            {
                                new MatchKeyword(JadeTokenType.Doctype, "doctype"),
                                new MatchKeyword(JadeTokenType.Case, "case"),
                                new MatchKeyword(JadeTokenType.When, "when"),
                                new MatchKeyword(JadeTokenType.Default, "default"),
                                new MatchKeyword(JadeTokenType.If, "if"),
                                new MatchKeyword(JadeTokenType.Else, "else"),
                                new MatchKeyword(JadeTokenType.Var, "- var"),
                                new MatchKeyword(JadeTokenType.Each, "each"),
                                new MatchKeyword(JadeTokenType.Mixin, "mixin"),
                                new MatchKeyword(JadeTokenType.Include, "include"),
                                new MatchKeyword(JadeTokenType.Class, "class"),
                            };

            // give each keyword the list of possible delimiters and not allow them to be 
            // substrings of other words, i.e. token fun should not be found in string "function"
            keywordmatchers.OfType<MatchKeyword>().ToList().ForEach(
                keyword =>
                {
                    keyword.AllowAsSubString = false;
                    keyword.SpecialCharacters = specialCharacters.Select(i => i as MatchKeyword).ToList();
                });

            matchers.Add(new MatchString(MatchString.DoubleQuote));
            matchers.Add(new MatchString(MatchString.SingleQuote));

            matchers.AddRange(specialCharacters);
            matchers.AddRange(keywordmatchers);

            matchers.AddRange(new List<StringMatcherBase>
                                        {
                                            new MatchWhiteSpace(),
                                            new MatchWord(specialCharacters)
                                        });

            return matchers;
        }
    }
}