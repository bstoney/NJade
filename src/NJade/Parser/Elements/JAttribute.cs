namespace NJade.Parser.Elements
{
    using Lexer;

    using NJade.Lexer.Tokenizer;
    using NJade.Render;

    /// <summary>
    /// Defines the Attribute class.
    /// </summary>
    internal class JAttribute : JElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="JAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public JAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(IXmlWriter writer)
        {
            writer.WriteAttributeString(this.Name, this.Value);
        }
    }
}