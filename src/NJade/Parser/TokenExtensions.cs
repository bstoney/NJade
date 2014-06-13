namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;
    using System.Text;

    using NJade.Lexer.Tokenizer;
    using NJade.Parser.Elements;
    using NJade.Lexer;

    /// <summary>
    /// Defines the TokenExtensions class.
    /// </summary>
    internal static class TokenExtensions
    {
        /// <summary>
        /// Raises the unexpected token.
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

            throw new NJadeException(string.Format("Unexpected {0} at line: {1}, column: {2}.", tokens.Current.TokenValue, tokens.Current.Line, tokens.Current.Column));
        }

        /// <summary>
        /// Gets the current token.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The value of the current token.</returns>
        public static string Get(this TokenStream tokens)
        {
            if (tokens.IsAtEnd())
            {
                return null;
            }

            var currentValue = tokens.Current.TokenValue;
            tokens.Consume();
            return currentValue;
        }

        /// <summary>
        /// Gets the current token .
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>The value of the current token.</returns>
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

            tokens.Consume();
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
        /// Determines whether the current token is any of the supplied token types.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenTypes">The token types.</param>
        /// <returns>true if the current token matches; otherwise, false.</returns>
        public static bool IsAny(this TokenStream tokens, params TokenType[] tokenTypes)
        {
            return tokenTypes.Contains(tokens.Current.TokenType);
        }

        /// <summary>
        /// Determines whether the current token is of the specified token type.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>true if the current token matches; otherwise, false.</returns>
        public static bool Is(this TokenStream tokens, TokenType tokenType)
        {
            return tokens.Current.With(t => t.TokenType == tokenType);
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>A list od jade elements.</returns>
        public static List<JElement> GetItems(this TokenStream tokens, int? indent)
        {
            var items = new List<JElement>();
            while (!tokens.IsAtEnd() && (indent == null || (tokens.Is(TokenType.WhiteSpace) && tokens.Current.TokenValue.Length > indent)))
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
    }
}