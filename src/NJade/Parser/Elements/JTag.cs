namespace NJade.Parser.Elements
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Monads;

    using NJade.Render;

    /// <summary>
    /// Defines the Element class.
    /// </summary>
    internal class JTag : JElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="JTag"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="classes">The classes.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="content">The content.</param>
        /// <param name="elements">The elements.</param>
        public JTag(string tag, string id, List<string> classes, List<JElement> attributes, JElement content, List<JElement> elements)
        {
            this.Id = id;
            this.Tag = tag;
            this.Classes = classes;
            this.Attributes = attributes;
            this.Content = content;
            this.Elements = elements;
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the classes.
        /// </summary>
        public List<string> Classes { get; private set; }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public List<JElement> Attributes { get; private set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public JElement Content { get; private set; }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        public List<JElement> Elements { get; private set; }

        /// <summary>
        /// Renders the element to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void Render(IXmlWriter writer)
        {
            writer.WriteStartElement(this.Tag);
            if (!string.IsNullOrEmpty(this.Id))
            {
                writer.WriteAttributeString("id", this.Id);
            }

            if (this.Classes.Any())
            {
                writer.WriteAttributeString("class", string.Join(" ", this.Classes));
            }

            foreach (var attribute in this.Attributes)
            {
                attribute.Render(writer);
            }

            this.Content.Do(c => c.Render(writer));

            foreach (var children in this.Elements)
            {
                children.Render(writer);
            }

            writer.WriteEndElement();
        }
    }
}