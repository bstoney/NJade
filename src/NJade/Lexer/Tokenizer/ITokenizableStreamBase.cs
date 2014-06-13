namespace NJade.Lexer.Tokenizer
{
    /// <summary>
    /// Defines the ITokenizableStreamBase interface.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public interface ITokenizableStreamBase<out TItem>
        where TItem : class
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        TItem Current { get; }

        /// <summary>
        /// The consume.
        /// </summary>
        void Consume();

        /// <summary>
        /// Determines whether the end of the stream has been reached.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        bool IsAtEnd();

        /// <summary>
        /// Peeks at a future item.
        /// </summary>
        /// <param name="lookahead">The look ahead.</param>
        /// <returns>
        /// The <see cref="TItem" />.
        /// </returns>
        TItem Peek(int lookahead = 1);

        /// <summary>
        /// The commit snapshot.
        /// </summary>
        void CommitSnapshot();

        /// <summary>
        /// The rollback snapshot.
        /// </summary>
        void RollbackSnapshot();

        /// <summary>
        /// The take snapshot.
        /// </summary>
        void TakeSnapshot();
    }
}