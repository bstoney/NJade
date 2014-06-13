namespace NJade.Parser
{
    using NJade.Lexer;

    /// <summary>
    /// Defines the Parser class.
    /// </summary>
    public class JadeParser
    {
        /// <summary>
        /// The lexer
        /// </summary>
        private readonly JadeLexer lexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="JadeParser"/> class.
        /// </summary>
        /// <param name="jade">The jade.</param>
        public JadeParser(string jade)
        {
            this.lexer = new JadeLexer(jade);
        }

        /// <summary>
        /// Gets the jade template.
        /// </summary>
        /// <returns>A new jade template.</returns>
        public Template GetJadeTemplate()
        {
            var tokens = new TokenStream(this.lexer.Tokenize());
            var jadeTemplate = Template.Produce(tokens);

            return jadeTemplate;
        }
    }
}
