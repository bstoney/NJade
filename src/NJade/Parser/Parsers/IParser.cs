namespace NJade.Parser.Parsers
{
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the IParser interface.
    /// </summary>
    internal interface IParser
    {
        /// <summary>
        /// Determines whether this instance can parse the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>true if the tokens can be parsed; otherwise, false.</returns>
        bool CanParse(TokenStream tokens);

        /// <summary>
        /// Parses the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A new element.</returns>
        JElement Parse(TokenStream tokens);
    }
}