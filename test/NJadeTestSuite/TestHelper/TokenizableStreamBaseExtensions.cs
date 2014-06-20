namespace NJadeTestSuite.TestHelper
{
    using System.Collections.Generic;

    using NJade.Lexer.Tokenizer;
    using NJade.Parser;

    /// <summary>
    /// Defines the TokenizableStreamBaseExtensions class.
    /// </summary>
    internal static class TokenizableStreamBaseExtensions
    {
        /// <summary>
        /// Gets all the tokens in the stream.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A list of tokens.</returns>
        public static List<TItem> GetAll<TItem>(this TokenizableStreamBase<TItem> tokens) 
            where TItem : class
        {
            var list = new List<TItem>();
            while (!tokens.IsAtEnd())
            {
                list.Add(tokens.Current);
                tokens.Consume();
            }

            return list;
        }

        /// <summary>
        /// Gets the hierarchy.
        /// </summary>
        /// <param name="tokenLines">The token lines.</param>
        /// <returns>The line hierarchy.</returns>
        public static LineHierarchy[] GetHierarchy(this TokenLineStream tokenLines)
        {
            var lines = new List<LineHierarchy>();
            while (!tokenLines.IsAtEnd())
            {
                lines.Add(new LineHierarchy()
                              {
                                  Children = tokenLines.Current.Children.GetHierarchy()
                              });
                tokenLines.Consume();
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Defines the LineHierarchy class.
        /// </summary>
        internal class LineHierarchy
        {
            /// <summary>
            /// Gets or sets the children.
            /// </summary>
            public LineHierarchy[] Children { get; set; }
        }
    }
}
