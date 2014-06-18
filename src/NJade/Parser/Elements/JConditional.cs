namespace NJade.Parser.Elements
{
    using System.Collections.Generic;
    using System.Linq;

    using NJade.Lexer.Tokenizer;
    using NJade.Render;
    using NJade.Lexer;

    /// <summary>
    /// Defines the JConditional class.
    /// </summary>
    internal class JConditional : JElement
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="JConditional"/> class from being created.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="trueElements">The true elements.</param>
        /// <param name="falseElements">The false elements.</param>
        private JConditional(JExpression expression, IEnumerable<JElement> trueElements, IEnumerable<JElement> falseElements)
        {
            this.Expression = expression;
            this.TrueElements = trueElements;
            this.FalseElements = falseElements;
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public JExpression Expression { get; private set; }

        /// <summary>
        /// Gets the true elements.
        /// </summary>
        public IEnumerable<JElement> TrueElements { get; private set; }

        /// <summary>
        /// Gets the false elements.
        /// </summary>
        public IEnumerable<JElement> FalseElements { get; private set; }

        /// <summary>
        /// Produces the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>
        /// A new if element.
        /// </returns>
        public static JElement Produce(TokenStream tokens, int indent)
        {
            tokens.Get(JadeTokenType.If);
            tokens.Get(TokenType.WhiteSpace);
            var expression = new JExpression(tokens.GetLine().AsString());
            var trueElements = tokens.GetItems(indent).ToList();
            var falseElements = Enumerable.Empty<JElement>();

            tokens.TakeSnapshot();
            if (tokens.Is(TokenType.WhiteSpace) && tokens.Get().Length == indent && tokens.Is(JadeTokenType.Else))
            {
                tokens.CommitSnapshot();
                tokens.GetLine();
                falseElements = tokens.GetItems(indent);
            }
            else
            {
                tokens.RollbackSnapshot();
            }

            return new JConditional(expression, trueElements, falseElements);
        }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(XmlWriter writer)
        {
            // TODO conditionals.
            foreach (var element in this.FalseElements)
            {
                element.Render(writer);
            }
        }
    }
}