namespace NJade.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using NJade.Lexer.Tokenizer;
    using NJade.Lexer.Tokenizer.Strings;

    /// <summary>
    /// Defines the TokenStream class.
    /// </summary>
    public class TokenStream : TokenizableStreamBase<StringToken> //// TODO (maybe), IEnumerable<Token>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenStream"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public TokenStream(IEnumerable<StringToken> tokens)
            : base(tokens.ToList)
        {
        }

        /////// <summary>
        /////// Returns an enumerator that iterates through the collection.
        /////// </summary>
        /////// <returns>
        /////// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /////// </returns>
        ////public IEnumerator<Token> GetEnumerator()
        ////{
        ////    return new TokenEnumerator(this);
        ////}

        /////// <summary>
        /////// Returns an enumerator that iterates through a collection.
        /////// </summary>
        /////// <returns>
        /////// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /////// </returns>
        ////IEnumerator IEnumerable.GetEnumerator()
        ////{
        ////    return new TokenEnumerator(this);
        ////}

        /////// <summary>
        /////// Defines the TokenEnumerator class.
        /////// </summary>
        ////public class TokenEnumerator : IEnumerator<Token>
        ////{
        ////    /// <summary>
        ////    /// The tokens.
        ////    /// </summary>
        ////    private readonly TokenStream tokens;

        ////    /// <summary>
        ////    /// Initializes a new instance of the <see cref="TokenEnumerator"/> class.
        ////    /// </summary>
        ////    /// <param name="tokenStream">The token stream.</param>
        ////    public TokenEnumerator(TokenStream tokenStream)
        ////    {
        ////        this.tokens = tokenStream;
        ////        this.tokens.TakeSnapshot();
        ////    }

        ////    /// <summary>
        ////    /// Gets the element in the collection at the current position of the enumerator.
        ////    /// </summary>
        ////    /// <returns>The element in the collection at the current position of the enumerator.</returns>
        ////    public Token Current { get; private set; }

        ////    /// <summary>
        ////    /// Gets the element in the collection at the current position of the enumerator.
        ////    /// </summary>
        ////    /// <returns>The element in the collection at the current position of the enumerator.</returns>
        ////    object IEnumerator.Current
        ////    {
        ////        get { return this.Current; }
        ////    }

        ////    /// <summary>
        ////    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ////    /// </summary>
        ////    public void Dispose()
        ////    {
        ////        this.tokens.CommitSnapshot();
        ////    }

        ////    /// <summary>
        ////    /// Advances the enumerator to the next element of the collection.
        ////    /// </summary>
        ////    /// <returns>
        ////    /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        ////    /// </returns>
        ////    public bool MoveNext()
        ////    {
        ////        if (this.tokens.IsAtEnd())
        ////        {
        ////            this.Current = null;
        ////            return false;
        ////        }

        ////        this.Current = this.tokens.Current;
        ////        this.tokens.Consume();
        ////        return true;
        ////    }

        ////    /// <summary>
        ////    /// Sets the enumerator to its initial position, which is before the first element in the collection.
        ////    /// </summary>
        ////    public void Reset()
        ////    {
        ////        this.tokens.RollbackSnapshot();
        ////        this.tokens.TakeSnapshot();
        ////    }
        ////}
    }
}