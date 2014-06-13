namespace NJade.Parser.Elements
{
    using NJade.Render;

    /// <summary>
    /// Defines the JExpression class.
    /// </summary>
    internal class JExpression : JElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="JExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public JExpression(string expression)
        {
            this.Expression = expression;
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(XmlWriter writer)
        {
            writer.WriteString(this.Expression);
        }
    }
}