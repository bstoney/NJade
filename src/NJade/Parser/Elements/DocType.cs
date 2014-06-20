namespace NJade.Parser.Elements
{
    using System.Monads;

    using NJade.Render;

    /// <summary>
    /// Defines the DocType class.
    /// </summary>
    public class DocType
    {
        /// <summary>
        /// The type
        /// </summary>
        private readonly string type;

        /// <summary>
        /// Initialises a new instance of the <see cref="DocType"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DocType(string type)
        {
            type.CheckNull("type");
            this.type = type;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <exception cref="NJade.NJadeException">Unexpected doc type  + this.type</exception>
        public override string ToString()
        {
            return this.type;
        }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal virtual void Render(IXmlWriter writer)
        {
            switch (this.type)
            {
                case "xml":
                    writer.WriteString("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    break;
                case "transitional":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                    break;
                case "strict":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
                    break;
                case "frameset":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">");
                    break;
                case "1.1":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
                    break;
                case "basic":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML Basic 1.1//EN\" \"http://www.w3.org/TR/xhtml-basic/xhtml-basic11.dtd\">");
                    break;
                case "mobile":
                    writer.WriteString("<!DOCTYPE html PUBLIC \"-//WAPFORUM//DTD XHTML Mobile 1.2//EN\" \"http://www.openmobilealliance.org/tech/DTD/xhtml-mobile12.dtd\">");
                    break;
                default:
                    writer.WriteString("<!DOCTYPE " + this.type + ">");
                    break;
            }
        }
    }
}