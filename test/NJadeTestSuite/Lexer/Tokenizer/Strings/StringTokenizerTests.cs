namespace NJadeTestSuite.Lexer.Tokenizer.Strings
{
    using Lexer.Tokenizer.Strings;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the StringTokenizerTests class.
    /// </summary>
    [TestClass]
    public class StringTokenizerTests
    {
        /// <summary>
        /// Tests that the string tokenizer should be created from a string.
        /// </summary>
        [TestMethod]
        public void ShouldBeCreatedFromAString()
        {
            new StringTokenizer("123");
        }

        /// <summary>
        /// Tests that the string tokenizer should be able to be created from an empty string.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToBeCreatedFromAnEmptyString()
        {
            new StringTokenizer(string.Empty);
        }

        /// <summary>
        /// Tests that the string tokenizer should consume characters in order.
        /// </summary>
        [TestMethod]
        public void ShouldConsumeCharactersInOrder()
        {
            var stringTokenizer = new StringTokenizer("123");

            Assert.AreEqual("1", stringTokenizer.Current);
            stringTokenizer.Consume();
            Assert.AreEqual("2", stringTokenizer.Current);
            stringTokenizer.Consume();
            Assert.AreEqual("3", stringTokenizer.Current);
        }

        /// <summary>
        /// Tests that the string tokenizer should be able to get the current location.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToGetTheCurrentLocation()
        {
            var stringTokenizer = new StringTokenizer("123");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(0, location.Index);
            Assert.AreEqual(1, location.Line);
            Assert.AreEqual(1, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should update the index when a character is consumed.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateTheIndexWhenACharacterIsConsumed()
        {
            var stringTokenizer = new StringTokenizer("123");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(0, location.Index);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Index);
        }

        /// <summary>
        /// Tests that the string tokenizer should the update the column index when a character is consumed.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateTheColumnIndexWhenACharacterIsConsumed()
        {
            var stringTokenizer = new StringTokenizer("123");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Column);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should update the line number when a character is consumed.
        /// </summary>
        [TestMethod]
        public void ShouldNotUpdateTheLineNumberUntilAfterANewLineCharacterIsConsumed()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);
        }

        /// <summary>
        /// Tests that the string tokenizer should not update the line number until after a dos new line character is consumed.
        /// </summary>
        [TestMethod]
        public void ShouldNotUpdateTheLineNumberUntilAfterADosNewLineCharacterIsConsumed()
        {
            var stringTokenizer = new StringTokenizer("1\r\n2");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);
        }

        /// <summary>
        /// Tests that the string tokenizer should reset the column index after a new line character is consumed.
        /// </summary>
        [TestMethod]
        public void ShouldResetTheColumnIndexAfterANewLineCharacterIsConsumed()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            Assert.AreEqual(1, location.Column);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
            Assert.AreEqual(2, location.Column);
            stringTokenizer.Consume();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);
            Assert.AreEqual(1, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should be able to take a snapshot.
        /// </summary>
        [TestMethod]
        public void ShouldBeAbleToTakeASnapshot()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Index);
            Assert.AreEqual(1, location.Line);
            Assert.AreEqual(2, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should retain the index after a snapshot is committed.
        /// </summary>
        [TestMethod]
        public void ShouldRetainTheIndexAfterASnapshotIsCommitted()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Index);
            
            stringTokenizer.CommitSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Index);
        }

        /// <summary>
        /// Tests that the string tokenizer should restore the index after a snapshot is rolledback.
        /// </summary>
        [TestMethod]
        public void ShouldRestoreTheIndexAfterASnapshotIsRolledback()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Index);

            stringTokenizer.RollbackSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(0, location.Index);
        }

        /// <summary>
        /// Tests that the string tokenizer should retain the column index after a snapshot is committed.
        /// </summary>
        [TestMethod]
        public void ShouldRetainTheColumnIndexAfterASnapshotIsCommitted()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Column);

            stringTokenizer.CommitSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should restore the column index after a snapshot is rolledback.
        /// </summary>
        [TestMethod]
        public void ShouldRestoreTheColumnIndexAfterASnapshotIsRolledback()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Column);

            stringTokenizer.RollbackSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Column);
        }

        /// <summary>
        /// Tests that the string tokenizer should retain the line number after a snapshot is committed.
        /// </summary>
        [TestMethod]
        public void ShouldRetainTheLineNumberAfterASnapshotIsCommitted()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);

            stringTokenizer.CommitSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);
        }

        /// <summary>
        /// Tests that the string tokenizer should restore the linenumber after a snapshot is rolledback.
        /// </summary>
        [TestMethod]
        public void ShouldRestoreTheLinenumberAfterASnapshotIsRolledback()
        {
            var stringTokenizer = new StringTokenizer("1\n2");

            stringTokenizer.TakeSnapshot();
            stringTokenizer.Consume();
            stringTokenizer.Consume();
            var location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(2, location.Line);

            stringTokenizer.RollbackSnapshot();
            location = stringTokenizer.GetCurrentLocation();
            Assert.AreEqual(1, location.Line);
        }
    }
}
