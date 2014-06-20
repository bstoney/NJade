namespace NJade.Parser.Elements
{
    using NJade.Render;

    /// <summary>
    /// Defines the JElement class.
    /// </summary>
    public abstract class JElement
    {
        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal abstract void Render(IXmlWriter writer);
    }
}
