namespace NJade.Parser.Elements
{
    using NJade.Render;

    /// <summary>
    /// Defines the Content class.
    /// </summary>
    internal class JText : JElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="JText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public JText(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(XmlWriter writer)
        {
            writer.WriteString(this.Text);
        }
    }
}