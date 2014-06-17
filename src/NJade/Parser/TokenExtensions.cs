namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Monads;
    using System.Text;

    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the TokenExtensions class.
    /// </summary>
    internal static class TokenExtensions
    {
        /// <summary>
        /// Raises an unexpected token exception.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void RaiseUnexpectedToken(this TokenStream tokens)
        {
            if (tokens.IsAtEnd())
            {
                throw new NJadeException("Unexpected end of template.");
            }

            if (tokens.Current == null)
            {
                throw new NJadeException("Invalid token.");
            }

            throw new NJadeException(
                string.Format(
                    "Unexpected {0} at line: {1}, column: {2}.",
                    tokens.Current.TokenValue,
                    tokens.Current.Line,
                    tokens.Current.Column));
        }

        /// <summary>
        /// Gets the value of the current token.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The value of the current token.</returns>
        public static string Get(this TokenStream tokens)
        {
            if (tokens.IsAtEnd())
            {
                return null;
            }

            if (tokens.Current == null)
            {
                throw new NJadeException("Invalid token.");
            }

            var currentValue = tokens.Current.TokenValue;
            tokens.Consume();
            return currentValue;
        }

        /// <summary>
        /// Gets the value of the current token if it matches the token type.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenType">Type of the Tokens.</param>
        /// <returns>The value of the current Tokens.</returns>
        public static string Get(this TokenStream tokens, TokenType tokenType)
        {
            if (!tokens.Is(tokenType))
            {
                tokens.RaiseUnexpectedToken();
            }

            return tokens.Get();
        }

        /// <summary>
        /// Gets all the tokens up to the end of the line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The value of all the tokens in the line.</returns>
        public static string GetLine(this TokenStream tokens)
        {
            var sb = new StringBuilder();
            while (!tokens.IsAtEnd() && !tokens.Is(JadeTokenType.NewLine))
            {
                sb.Append(tokens.Get());
            }

            if (!tokens.IsAtEnd())
            {
                tokens.Consume();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Consumes any of the supplied token types.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenTypes">The token types.</param>
        public static void ConsumeAny(this TokenStream tokens, params TokenType[] tokenTypes)
        {
            if (tokens.IsAny(tokenTypes))
            {
                tokens.Consume();
            }
            else
            {
                tokens.RaiseUnexpectedToken();
            }
        }

        /// <summary>
        /// Determines whether the current tokent is any of the supplied token types.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenTypes">The token types.</param>
        /// <returns>true if the current token matches; otherwise, false.</returns>
        public static bool IsAny(this TokenStream tokens, params TokenType[] tokenTypes)
        {
            return !tokens.IsAtEnd() && tokens.Current.With(t => tokenTypes.Contains(t.TokenType));
        }

        /// <summary>
        /// Determines whether the current token is of the specified token type.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>true if the current token matches; otherwise, false.</returns>
        public static bool Is(this TokenStream tokens, TokenType tokenType)
        {
            return !tokens.IsAtEnd() && tokens.Current.With(t => t.TokenType == tokenType);
        }

        /// <summary>
        /// Peeks at the remaining tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A list of tokens.</returns>
        public static List<StringToken> PeekAtRemainingTokens(this TokenStream tokens)
        {
            var tokenList = new List<StringToken>();
            tokens.TakeSnapshot();

            while (!tokens.IsAtEnd())
            {
                tokenList.Add(tokens.Current);
                tokens.Consume();
            }

            tokens.RollbackSnapshot();
            return tokenList;
        }

        /// <summary>
        /// Gets all the elements where the intent is greated than the indent supplied.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>A list of jade elements.</returns>
        public static List<JElement> GetItems(this TokenStream tokens, int? indent)
        {
            var items = new List<JElement>();
            while (!tokens.IsAtEnd()
                   && (indent == null || (tokens.Is(TokenType.WhiteSpace) && tokens.Current.TokenValue.Length > indent)))
            {
                var thisIndent = 0;
                if (tokens.Current.TokenType == TokenType.WhiteSpace)
                {
                    thisIndent = tokens.Get().Length;
                }

                if (tokens.Is(JadeTokenType.NewLine))
                {
                    tokens.Consume();
                    continue;
                }

                if (tokens.Is(JadeTokenType.If))
                {
                    items.Add(JConditional.Produce(tokens, thisIndent));
                    continue;
                }

                var element = JTag.Produce(tokens, thisIndent);
                if (element == null)
                {
                    tokens.RaiseUnexpectedToken();
                }

                items.Add(element);
            }

            return items;
        }

        ////    /// <summary>
        ////    /// Gets the child tokens.
        ////    /// </summary>
        ////    /// <param name="tokens">The tokens.</param>
        ////    /// <param name="indent">The indent.</param>
        ////    /// <returns>A new Tokens stream.</returns>
        ////    public static TokenStream GetChildTokens(this TokenStream tokens, int? indent)
        ////    {
        ////        var tokenList = new List<StringToken>();
        ////        while (!tokens.IsAtEnd() && (indent == null || (tokens.Is(TokenType.WhiteSpace) && tokens.Current.TokenValue.Length > indent)))
        ////        {
        ////            tokenList.Add(tokens.Current);
        ////        }

        ////        return new TokenStream(tokenList);
        ////    }

        ////    public static IEnumerable<TokenHierarchy> GetTokenHierarchy(this TokenStream tokens, int? indent)
        ////    {
        ////        while (!tokens.IsAtEnd())
        ////        {
        ////            var current = tokens.Current;
        ////            var children = tokens.GetChildTokens(indent ?? 0).GetTokenHierarchy(indent ?? 0);

        ////            yield return new TokenHierarchy(current, children);
        ////        }
        ////    }
        ////}

        /////// <summary>
        /////// Defines the TokenHierarchy class.
        /////// </summary>
        ////internal class TokenHierarchy
        ////{
        ////    /// <summary>
        ////    /// Initializes a new instance of the <see cref="TokenHierarchy"/> class.
        ////    /// </summary>
        ////    /// <param name="tokens">The Tokens.</param>
        ////    /// <param name="children">The children.</param>
        ////    public TokenHierarchy(IEnumerable<StringToken> tokens, IEnumerable<TokenHierarchy> children)
        ////    {
        ////        this.Tokens = new TokenStream(tokens);
        ////        this.Children = children;
        ////    }

        ////    /// <summary>
        ////    /// Gets the Tokens.
        ////    /// </summary>
        ////    /// <value>
        ////    /// The Tokens.
        ////    /// </value>
        ////    public TokenStream Tokens { get; private set; }

        ////    /// <summary>
        ////    /// Gets or sets the children.
        ////    /// </summary>
        ////    /// <value>
        ////    /// The children.
        ////    /// </value>
        ////    public IEnumerable<TokenHierarchy> Children { get; private set; }
        ////}

        ////internal class JElementCollection
        ////{
        ////}
    }
}