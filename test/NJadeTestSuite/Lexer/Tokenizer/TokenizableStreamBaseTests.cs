namespace NJadeTestSuite.Lexer.Tokenizer
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer;

    /// <summary>
    /// Defines the TokenizableStreamBaseTests class.
    /// </summary>
    [TestClass]
    public class TokenizableStreamBaseTests
    {
        /// <summary>
        /// Tests that the tokenizable stream base should be create from an extractor method.
        /// </summary>
        [TestMethod]
        public void ShouldBeCreateFromAnExtractorMethod()
        {
            new TokenizableStreamBase<object>(() => Enumerable.Empty<object>().ToList());
        }

        /// <summary>
        /// Tests that the tokenizable stream base should consume tokens from the extractor.
        /// </summary>
        [TestMethod]
        public void ShouldConsumeTokensFromTheExtractor()
        {
            var items = new List<object> { 1, 2, 3 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.Consume();
            Assert.AreEqual(2, tokenizableStreamBase.Current);
            tokenizableStreamBase.Consume();
            Assert.AreEqual(3, tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to determine when the last item has been consumed.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToDetermineWhenTheLastItemHasBeenConsumed()
        {
            var items = new List<object> { 1 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.IsFalse(tokenizableStreamBase.IsAtEnd());
            tokenizableStreamBase.Consume();
            Assert.IsTrue(tokenizableStreamBase.IsAtEnd());
            tokenizableStreamBase.Consume();
            Assert.IsTrue(tokenizableStreamBase.IsAtEnd());
        }

        /// <summary>
        /// Tests that the tokenizable stream base should return null from current after last item hase been consumed.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullFromCurrentAfterLastItemHaseBeenConsumed()
        {
            var items = new List<object> { 1 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.IsNotNull(tokenizableStreamBase.Current);
            tokenizableStreamBase.Consume();
            Assert.IsNull(tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to peek at the next item.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToPeekAtTheNextItem()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            Assert.AreEqual(2, tokenizableStreamBase.Peek());
        }

        /// <summary>
        /// Tests that the tokenizable stream base should bea ble to peek without consuming the current item.
        /// </summary>
        [TestMethod]
        public void ShouldBeaBleToPeekWithoutConsumingTheCurrentItem()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            Assert.AreEqual(2, tokenizableStreamBase.Peek());
            Assert.AreEqual(1, tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to peek at future items.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToPeekAtFutureItems()
        {
            var items = new List<object> { 1, 2, 3, 4, 5, 6 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            Assert.AreEqual(4, tokenizableStreamBase.Peek(3));
            Assert.AreEqual(6, tokenizableStreamBase.Peek(5));
        }

        /// <summary>
        /// Tests that the tokenizable stream base should return null when peeking after the last item.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenPeekingAfterTheLastItem()
        {
            var items = new List<object> { 1 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            Assert.IsNull(tokenizableStreamBase.Peek());
            Assert.IsNull(tokenizableStreamBase.Peek(3));
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to take a snapshot.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToTakeASnapshot()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.TakeSnapshot();
            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.Consume();
            Assert.AreEqual(2, tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to commit a snapshot.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToCommitASnapshot()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.TakeSnapshot();
            tokenizableStreamBase.Consume();
            Assert.AreEqual(2, tokenizableStreamBase.Current);
            tokenizableStreamBase.CommitSnapshot();
            Assert.AreEqual(2, tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to rollback a snapshot.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToRollbackASnapshot()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.TakeSnapshot();
            tokenizableStreamBase.Consume();
            Assert.AreEqual(2, tokenizableStreamBase.Current);
            tokenizableStreamBase.RollbackSnapshot();
            Assert.AreEqual(1, tokenizableStreamBase.Current);
        }

        /// <summary>
        /// Tests that the tokenizable stream base should be able to rollback a snapshot after the last item has been consumed.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToRollbackASnapshotAfterTheLastItemHasBeenConsumed()
        {
            var items = new List<object> { 1, 2 };
            var tokenizableStreamBase = new TokenizableStreamBase<object>(() => items);

            Assert.AreEqual(1, tokenizableStreamBase.Current);
            tokenizableStreamBase.TakeSnapshot();
            tokenizableStreamBase.Consume();
            tokenizableStreamBase.Consume();
            tokenizableStreamBase.Consume();
            Assert.IsTrue(tokenizableStreamBase.IsAtEnd());
            tokenizableStreamBase.RollbackSnapshot();
            Assert.AreEqual(1, tokenizableStreamBase.Current);
        }
    }
}
