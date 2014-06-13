namespace NJadeTestSuite.Renderer
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer.Strings;
    using NJade.Parser;
    using NJade.Render;

    /// <summary>
    /// Defines the StringRendererTests class.
    /// </summary>
    [TestClass]
    public class StringRendererTests
    {
        /// <summary>
        /// Tests that the string renderer should thrown an argument null exception when the template is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrownAnArgumentNullExceptionWhenTheTemplateIsNull()
        {
            new StringRenderer(null);
        }

        /// <summary>
        /// Tests that the string renderer should return a string when the template is rendered.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAStringWhenTheTemplateIsRendered()
        {
            var tokens = new TokenStream(Enumerable.Empty<StringToken>());
            var renderer = new StringRenderer(Template.Produce(tokens));

            var text = renderer.Render();

            Assert.IsNotNull(text);
        }
    }
}
