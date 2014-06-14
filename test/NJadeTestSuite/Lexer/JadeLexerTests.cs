namespace NJadeTestSuite.Lexer
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer;

    /// <summary>
    /// Defines the JadeLexerTests class.
    /// </summary>
    [TestClass]
    public class JadeLexerTests
    {
        /// <summary>
        /// Tests that the jade lexer should normalize source when constructing a jade lexer.
        /// </summary>
        [TestMethod]
        public void ShouldNormalizeSourceWhenConstructingAJadeLexer()
        {
            var jadeLexerUnnomalized = new JadeLexer("A \r\n unnormalized \r\n string");
            var tokensUnnormalized = jadeLexerUnnomalized.Tokenize().ToArray();
            var jadeLexerNomalized = new JadeLexer("A \n unnormalized \n string");
            var tokensNormalized = jadeLexerNomalized.Tokenize().ToArray();
            CollectionAssert.AreEqual(tokensNormalized, tokensUnnormalized);
        }
    }
}
