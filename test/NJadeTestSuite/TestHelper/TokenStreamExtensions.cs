using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJadeTestSuite.TestHelper
{
    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;

    /// <summary>
    /// Defines the TokenStreamExtensions class.
    /// </summary>
    internal static class TokenStreamExtensions
    {
        /// <summary>
        /// Gets all the tokens in the stream.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A list of tokens.</returns>
        public static List<StringToken> GetAll(this TokenStream tokens)
        {
            var list = new List<StringToken>();
            while (!tokens.IsAtEnd())
            {
                list.Add(tokens.Current);
                tokens.Consume();
            }

            return list;
        }
    }
}
