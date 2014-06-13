namespace NJade.Parser.Elements
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;
    using System.Text;

    using NJade.Lexer.Tokenizer;
    using NJade.Render;
    using NJade.Lexer;

    /// <summary>
    /// Defines the Element class.
    /// </summary>
    internal class JTag : JElement
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="JTag" /> class from being created.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="classes">The classes.</param>
        /// <param name="attributes"></param>
        /// <param name="content">The content.</param>
        /// <param name="elements">The elements.</param>
        private JTag(string tag, string id, List<string> classes, List<JElement> attributes, JElement content, List<JElement> elements)
        {
            this.Id = id;
            this.Tag = tag;
            this.Classes = classes;
            this.Attributes = attributes;
            this.Content = content;
            this.Elements = elements;
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the classes.
        /// </summary>
        public List<string> Classes { get; private set; }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public List<JElement> Attributes { get; private set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public JElement Content { get; private set; }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        public List<JElement> Elements { get; private set; }

        /// <summary>
        /// Produces the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>
        /// A new element.
        /// </returns>
        public static JElement Produce(TokenStream tokens, int indent)
        {
            if (!tokens.IsAny(TokenType.Word, JadeTokenType.Dot, JadeTokenType.HashTag))
            {
                tokens.RaiseUnexpectedToken();
            }

            var tag = "div";
            string id = null;
            var classes = new List<string>();
            var attributes = new List<JElement>();
            JElement content = null;
            var elements = new List<JElement>();
            if (tokens.Is(TokenType.Word))
            {
                tag = tokens.Get();
            }

            classes.AddRange(GetClasses(tokens, ref id));

            attributes.AddRange(GetAttributes(tokens));

            classes.AddRange(GetClasses(tokens, ref id));

            if (tokens.Is(JadeTokenType.InlineValue))
            {
                content = new JExpression(tokens.GetLine());
            }
            else if (tokens.Is(TokenType.WhiteSpace))
            {
                tokens.Consume();
                content = new JText(tokens.GetLine());
            }
            else if (tokens.Is(JadeTokenType.Block))
            {
                tokens.Consume();
                content = new JText(GetBlock(tokens, indent));
            }
            else if (tokens.Is(JadeTokenType.Colon))
            {
                tokens.Consume();
                tokens.ConsumeAny(TokenType.WhiteSpace);
                elements.Add(Produce(tokens, indent));
            }
            else
            {
                if (tokens.Is(TokenType.WhiteSpace))
                {
                    tokens.Get();
                }

                tokens.Get(JadeTokenType.NewLine);
                elements.AddRange(tokens.GetItems(indent));
            }

            return new JTag(tag, id, classes, attributes, content, elements);
        }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(XmlWriter writer)
        {
            writer.WriteStartElement(this.Tag);
            if (!string.IsNullOrEmpty(this.Id))
            {
                writer.WriteAttributeString("id", this.Id);
            }

            if (this.Classes.Any())
            {
                writer.WriteAttributeString("class", string.Join(" ", this.Classes));
            }

            foreach (var attribute in this.Attributes)
            {
                attribute.Render(writer);
            }

            this.Content.Do(c => c.Render(writer));

            foreach (var children in this.Elements)
            {
                children.Render(writer);
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A list of attributes.</returns>
        private static IEnumerable<JElement> GetAttributes(TokenStream tokens)
        {
            if (tokens.Is(JadeTokenType.OpenParenth))
            {
                tokens.Consume();
                while (!tokens.Is(JadeTokenType.CloseParenth))
                {
                    yield return JAttribute.Produce(tokens);
                }

                tokens.Consume();
            }
        }

        /// <summary>
        /// Gets the classes.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>A list of classes.</returns>
        private static IEnumerable<string> GetClasses(TokenStream tokens, ref string id)
        {
            var classes = new List<string>();
            while (tokens.IsAny(JadeTokenType.Dot, JadeTokenType.HashTag))
            {
                if (tokens.Is(JadeTokenType.Dot))
                {
                    tokens.Consume();
                    classes.Add(tokens.Get(TokenType.Word));
                }
                else if (id == null)
                {
                    tokens.Consume();
                    id = tokens.Get(TokenType.Word);
                }
                else
                {
                    tokens.RaiseUnexpectedToken();
                }
            }

            return classes;
        }

        /// <summary>
        /// Gets the block.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>A string.</returns>
        private static string GetBlock(TokenStream tokens, int indent)
        {
            var sb = new StringBuilder();
            int? blockIndent = null;
            while (!tokens.IsAtEnd() && tokens.Is(TokenType.WhiteSpace) && tokens.Current.TokenValue.Length > indent)
            {
                if (blockIndent == null)
                {
                    blockIndent = tokens.Current.TokenValue.Length;
                }

                sb.AppendLine(tokens.GetLine().Substring(blockIndent.Value));
            }

            return sb.ToString().TrimEnd();
        }
    }
}