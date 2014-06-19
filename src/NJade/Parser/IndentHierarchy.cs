namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    /// <summary>
    /// Defines the IndentHierarchy class.
    /// </summary>
    internal class IndentHierarchy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndentHierarchy"/> class.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public IndentHierarchy(TokenStream line)
            : this(line, Enumerable.Empty<IndentHierarchy>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndentHierarchy" /> class.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="children">The children.</param>
        public IndentHierarchy(TokenStream line, IEnumerable<IndentHierarchy> children)
        {
            line.CheckNull("line");
            children.CheckNull("children");
            
            this.Line = line;
            this.Children = children.ToArray();
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        public TokenStream Line { get; private set; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public IndentHierarchy[] Children { get; private set; }
    }
}