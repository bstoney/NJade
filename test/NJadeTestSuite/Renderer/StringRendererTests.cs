namespace NJadeTestSuite.Renderer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NJade.Parser;
    using NJade.Parser.Elements;
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
        /// Tests that the string renderer should call document type render when the template is rendered.
        /// </summary>
        [TestMethod]
        public void ShouldCallDocTypeRenderWhenTheTemplateIsRendered()
        {
            var docType = new Mock<DocType>("html");
            var template = Mock.Of<ITemplate>(t =>
                t.Elements == new List<JElement>() &&
                t.DocType == docType.Object);
            var renderer = new StringRenderer(template);

            renderer.Render();

            docType.Verify(d => d.Render(It.IsAny<IXmlWriter>()), Times.Once);
        }

        /// <summary>
        /// Tests that the string renderer should call render for each element when the template is rendered.
        /// </summary>
        [TestMethod]
        public void ShouldCallRenderForEachElementWhenTheTemplateIsRendered()
        {
            var element1 = Mock.Of<JElement>();
            var element2 = Mock.Of<JElement>();
            var template = Mock.Of<ITemplate>(t => t.Elements == new List<JElement> { element1, element2 });
            var renderer = new StringRenderer(template);

            renderer.Render();

            Mock.Get(element1).Verify(e => e.Render(It.IsAny<IXmlWriter>()), Times.Once);
            Mock.Get(element2).Verify(e => e.Render(It.IsAny<IXmlWriter>()), Times.Once);
        }

        /// <summary>
        /// Tests that the string renderer should return a string when the template is rendered.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAStringWhenTheTemplateIsRendered()
        {
            var renderer = new StringRenderer(Mock.Of<ITemplate>(t => t.Elements == new List<JElement>()));

            var text = renderer.Render();

            Assert.IsNotNull(text);
        }
    }
}
