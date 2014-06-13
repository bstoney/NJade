namespace NJade.Lexer.Tokenizer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the TokenizableStreamBase class.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class TokenizableStreamBase<TItem> : ITokenizableStreamBase<TItem>
        where TItem : class
    {
        /// <summary>
        /// The items
        /// </summary>
        private readonly List<TItem> items;

        /// <summary>
        /// The snapshot indexes
        /// </summary>
        private readonly Stack<int> snapshotIndexes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizableStreamBase{TItem}"/> class. 
        /// </summary>
        /// <param name="extractor">
        /// The extractor.
        /// </param>
        public TokenizableStreamBase(Func<List<TItem>> extractor)
        {
            this.Index = 0;
            this.items = extractor();
            this.snapshotIndexes = new Stack<int>();
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public virtual TItem Current
        {
            get
            {
                return this.Eof(0) ? null : this.items[this.Index];
            }
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        protected int Index { get; private set; }

        /// <summary>
        /// The consume.
        /// </summary>
        public virtual void Consume()
        {
            this.Index++;
        }

        /// <summary>
        /// Determines whether the end of the stream has been reached.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public virtual bool IsAtEnd()
        {
            return this.Eof(0);
        }

        /// <summary>
        /// Peeks at a future item.
        /// </summary>
        /// <param name="lookahead">The look ahead.</param>
        /// <returns>
        /// The <see cref="TItem" />.
        /// </returns>
        public virtual TItem Peek(int lookahead = 1)
        {
            return this.Eof(lookahead) ? null : this.items[this.Index + lookahead];
        }

        /// <summary>
        /// The commit snapshot.
        /// </summary>
        public virtual void CommitSnapshot()
        {
            this.snapshotIndexes.Pop();
        }

        /// <summary>
        /// The rollback snapshot.
        /// </summary>
        public virtual void RollbackSnapshot()
        {
            this.Index = this.snapshotIndexes.Pop();
        }

        /// <summary>
        /// The take snapshot.
        /// </summary>
        public virtual void TakeSnapshot()
        {
            this.snapshotIndexes.Push(this.Index);
        }

        /// <summary>
        /// The EOF.
        /// </summary>
        /// <param name="lookahead">The look ahead.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        private bool Eof(int lookahead)
        {
            return this.Index + lookahead >= this.items.Count;
        }
    }
}