namespace NJade.Render
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Monads;
    using System.Text;

    using NJade.Parser;
    using NJade.Parser.Elements;

    /// <summary>
    /// Defines the StringRenderer class.
    /// </summary>
    public class StringRenderer
    {
        /// <summary>
        /// The jade template
        /// </summary>
        private readonly Template template;

        /// <summary>
        /// Initialises a new instance of the <see cref="StringRenderer" /> class.
        /// </summary>
        /// <param name="template">The jade template.</param>
        public StringRenderer(Template template)
        {
            template.CheckNull("template");
            this.template = template;
        }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        public string Render()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var writer = new XmlWriter(stringWriter))
            {
                this.template.DocType.Do(d => d.Render(writer));
                foreach (var item in this.template.Elements)
                {
                    item.Render(writer);
                }
            }

            return sb.ToString();
        }
    }
}
