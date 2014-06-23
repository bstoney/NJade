namespace NJade.Parser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using NJade.Lexer;
    using NJade.Lexer.Tokenizer;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the JTagParser class.
    /// </summary>
    internal class JTagParser : IParser
    {
        /// <summary>
        /// The attributes.
        /// </summary>
        private List<JElement> attributes;

        /// <summary>
        /// The classes.
        /// </summary>
        private List<string> classes;

        /// <summary>
        /// The identifier.
        /// </summary>
        private string id;

        /// <summary>
        /// The tag.
        /// </summary>
        private string tag;

        /// <summary>
        /// The content.
        /// </summary>
        private JElement content;

        /// <summary>
        /// Determines whether this instance can parse the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// true if the tokens can be parsed; otherwise, false.
        /// </returns>
        public bool CanParse(TokenStream tokens)
        {
            return tokens.IsAny(TokenType.Word, JadeTokenType.Dot, JadeTokenType.HashTag);
        }

        /// <summary>
        /// Parses the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A new element.
        /// </returns>
        public JElement Parse(TokenStream tokens)
        {
            this.tag = "div";
            this.id = null;
            this.classes = new List<string>();
            this.attributes = new List<JElement>();
            this.content = null;
            var elements = new List<JElement>();
            if (tokens.Is(TokenType.Word))
            {
                this.tag = tokens.Get();
            }

            this.ParseClasses(tokens);
            this.ParseAttributes(tokens);
            this.ParseClasses(tokens);

            ////if (tokens.Is(JadeTokenType.InlineValue))
            ////{
            ////    content = new JExpression(tokens.GetLine().AsString());
            ////}
            ////else if (tokens.Is(TokenType.WhiteSpace))
            ////{
            ////    tokens.Consume();
            ////    content = new JText(tokens.GetLine().AsString());
            ////}
            ////else if (tokens.Is(JadeTokenType.Block))
            ////{
            ////    tokens.Consume();
            ////    content = new JText(GetBlock(tokens, indent));
            ////}
            ////else if (tokens.Is(JadeTokenType.Colon))
            ////{
            ////    tokens.Consume();
            ////    tokens.GetAny(TokenType.WhiteSpace);
            ////    elements.Add(Produce(tokens, indent));
            ////}
            ////else
            ////{
            ////    if (tokens.Is(TokenType.WhiteSpace))
            ////    {
            ////        tokens.Get();
            ////    }

            ////    tokens.GetAny(JadeTokenType.NewLine);
            ////    elements.AddRange(tokens.GetItems(indent));
            ////}

            return new JTag(this.tag, this.id, this.classes, this.attributes, this.content, elements);
        }

        /// <summary>
        /// Parse the classes.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void ParseClasses(TokenStream tokens)
        {
            while (tokens.IsAny(JadeTokenType.Dot, JadeTokenType.HashTag))
            {
                if (tokens.Is(JadeTokenType.Dot))
                {
                    tokens.Consume();
                    this.classes.Add(tokens.GetAny(TokenType.Word));
                }
                else if (this.id == null)
                {
                    tokens.Consume();
                    this.id = tokens.GetAny(TokenType.Word);
                }
                else
                {
                    tokens.RaiseUnexpectedToken();
                }
            }
        }

        /// <summary>
        /// Parse the attributes.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void ParseAttributes(TokenStream tokens)
        {
            if (tokens.Is(JadeTokenType.OpenParenth))
            {
                tokens.Consume();
                while (!tokens.Is(JadeTokenType.CloseParenth))
                {
                    var name = tokens.GetAny(TokenType.Word);
                    tokens.GetAny(JadeTokenType.Equals);

                    // TODO variables, arrays, other.
                    var value = tokens.GetAny(TokenType.QuotedString);

                    if (string.Equals(name, "class", StringComparison.OrdinalIgnoreCase))
                    {
                        this.classes.Add(value);
                    }
                    else
                    {
                        this.attributes.Add(new JAttribute(name, value));
                    }
                }

                tokens.Consume();
            }
        }

        /////// <summary>
        /////// Gets the block.
        /////// </summary>
        /////// <param name="tokens">The tokens.</param>
        /////// <param name="indent">The indent.</param>
        /////// <returns>A string.</returns>
        ////private static string GetBlock(TokenStream tokens, int indent)
        ////{
        ////    var sb = new StringBuilder();
        ////    int? blockIndent = null;
        ////    while (!tokens.IsAtEnd() && tokens.Is(TokenType.WhiteSpace) && tokens.Current.TokenValue.Length > indent)
        ////    {
        ////        if (blockIndent == null)
        ////        {
        ////            blockIndent = tokens.Current.TokenValue.Length;
        ////        }

        ////        sb.AppendLine(tokens.GetLine().AsString().Substring(blockIndent.Value));
        ////    }

        ////    return sb.ToString().TrimEnd();
        ////}
    }
}
