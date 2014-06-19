namespace NJade.Parser
{
    using System;
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
        /// Gets the underlying string value of the stream without consuming the tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A string</returns>
        public static string AsString(this TokenStream tokens)
        {
            tokens.TakeSnapshot();
            var sb = new StringBuilder();
            try
            {
                while (!tokens.IsAtEnd())
                {
                    sb.Append(tokens.Get());
                }
            }
            finally
            {
                tokens.RollbackSnapshot();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Raises an unexpected token exception.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <exception cref="NJadeException">
        /// Unexpected end of template.
        /// or
        /// Invalid token.
        /// or
        /// Unexpected token.
        /// </exception>
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
        /// Determines whether the current token is any of the supplied token types.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenTypes">The token types.</param>
        /// <returns>true if the current token matches; otherwise, false.</returns>
        public static bool IsAny(this TokenStream tokens, params TokenType[] tokenTypes)
        {
            tokenTypes.CheckNull("tokenTypes");
            return !tokens.IsAtEnd() && tokens.Current.With(t => tokenTypes.Contains(t.TokenType));
        }

        /// <summary>
        /// Gets the value of the current token.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// The value of the current token.
        /// </returns>
        /// <exception cref="NJadeException">
        /// Unexpected end of template.
        /// or
        /// Invalid token.
        /// </exception>
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

            var currentToken = tokens.Current;
            tokens.Consume();
            return currentToken.TokenValue;
        }

        /// <summary>
        /// Gets the value of the current token if it matches any of the supplied token types.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tokenTypes">The token types.</param>
        /// <returns>
        /// The value of the current token.
        /// </returns>
        /// <exception cref="NJadeException">
        /// Unexpected end of template.
        /// or
        /// Invalid token.
        /// or
        /// Unexpected token.
        /// </exception>
        public static string GetAny(this TokenStream tokens, params TokenType[] tokenTypes)
        {
            if (!tokens.IsAny(tokenTypes))
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
        public static TokenStream GetLine(this TokenStream tokens)
        {
            var line = new List<StringToken>();
            while (!tokens.IsAtEnd() && !tokens.Is(JadeTokenType.NewLine))
            {
                line.Add(tokens.Current);
                tokens.Consume();
            }

            if (!tokens.IsAtEnd())
            {
                tokens.Consume();
            }

            return new TokenStream(line);
        }

        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A list of the lines.</returns>
        public static List<TokenStream> GetLines(this TokenStream tokens)
        {
            var lines = new List<TokenStream>();
            while (!tokens.IsAtEnd())
            {
                lines.Add(tokens.GetLine());
            }

            return lines;
        }

        /// <summary>
        /// Gets the indent hierachy.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The indent hierarchy.</returns>
        public static IndentHierarchy[] GetIndentHierachy(this TokenStream tokens)
        {
            Func<int> getCurrentIndent = () => tokens.Is(TokenType.WhiteSpace) ? tokens.Current.TokenValue.Length : 0;

            var lines = new List<IndentHierarchy>();
            var indent = getCurrentIndent();
            while (!tokens.IsAtEnd())
            {
                var currentLine = tokens.GetLine();
                var indentHierarchy = getCurrentIndent() > indent
                                          ? new IndentHierarchy(currentLine, tokens.GetIndentHierachy())
                                          : new IndentHierarchy(currentLine);
                lines.Add(indentHierarchy);

                if (getCurrentIndent() < indent)
                {
                    break;
                }
            }

            return lines.ToArray();
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

        /////// <summary>
        /////// Peeks at the remaining tokens.
        /////// </summary>
        /////// <param name="tokens">The tokens.</param>
        /////// <returns>A list of tokens.</returns>
        ////public static List<StringToken> PeekAtRemainingTokens(this TokenStream tokens)
        ////{
        ////    var tokenList = new List<StringToken>();
        ////    tokens.TakeSnapshot();

        ////    while (!tokens.IsAtEnd())
        ////    {
        ////        tokenList.Add(tokens.Current);
        ////        tokens.Consume();
        ////    }

        ////    tokens.RollbackSnapshot();
        ////    return tokenList;
        ////}

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