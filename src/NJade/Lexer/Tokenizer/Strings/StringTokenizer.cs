namespace NJade.Lexer.Tokenizer.Strings
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the StringTokenizer class.
    /// </summary>
    public class StringTokenizer : TokenizableStreamBase<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTokenizer"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public StringTokenizer(string source) :
            base(() => source.Select(i => i.ToString(CultureInfo.InvariantCulture))
                             .ToList())
        {
            this.Line = 1;
            this.Column = 1;
            this.SnapshotLine = new Stack<int>();
            this.SnapshotColumn = new Stack<int>();
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        protected int Line { get; private set; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        protected int Column { get; private set; }

        /// <summary>
        /// Gets the snapshot line.
        /// </summary>
        protected Stack<int> SnapshotLine { get; private set; }

        /// <summary>
        /// Gets the snapshot column.
        /// </summary>
        protected Stack<int> SnapshotColumn { get; private set; }

        /// <summary>
        /// Consumes this instance.
        /// </summary>
        public override void Consume()
        {
            if (this.Current == "\n")
            {
                this.Line++;
                this.Column = 0;
            }

            base.Consume();
            this.Column++;
        }

        /// <summary>
        /// Takes the snapshot.
        /// </summary>
        public override void TakeSnapshot()
        {
            base.TakeSnapshot();
            this.SnapshotLine.Push(this.Line);
            this.SnapshotColumn.Push(this.Column);
        }

        /// <summary>
        /// Commits the snapshot.
        /// </summary>
        public override void CommitSnapshot()
        {
            base.CommitSnapshot();
            this.SnapshotLine.Pop();
            this.SnapshotColumn.Pop();
        }

        /// <summary>
        /// Rollbacks the snapshot.
        /// </summary>
        public override void RollbackSnapshot()
        {
            base.RollbackSnapshot();
            this.Line = this.SnapshotLine.Pop();
            this.Column = this.SnapshotColumn.Pop();
        }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        /// <returns>The current location.</returns>
        public virtual ITokenLocation GetCurrentLocation()
        {
            return new TokenLocation(this.Index, this.Line, this.Column);
        }
    }
}