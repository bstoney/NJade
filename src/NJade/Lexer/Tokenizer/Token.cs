namespace NJade.Lexer.Tokenizer
{
    using System;
    using System.Collections.Generic;
    using System.Monads;

    /// <summary>
    /// Defines the Token class.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Token<TValue> : IEquatable<Token<TValue>>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Token{TValue}"/> class. 
        /// </summary>
        /// <param name="tokenType">
        /// Type of the token.
        /// </param>
        internal Token(TokenType tokenType)
        {
            tokenType.CheckNull("tokenType");
            this.TokenType = tokenType;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Token{TValue}" /> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        internal Token(TokenType tokenType, int index)
            : this(tokenType)
        {
            this.Index = index;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Token{TValue}" /> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal Token(TokenType tokenType, int index, TValue value)
            : this(tokenType, index)
        {
            this.TokenValue = value;
        }

        /// <summary>
        /// Gets the token value.
        /// </summary>
        public TValue TokenValue { get; private set; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public TokenType TokenType { get; private set; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int? Index { get; private set; }

        /// <summary>
        /// Determines whether two specified tokens have the same value.
        /// </summary>
        /// <param name="a">The first token to compare, or null. </param>
        /// <param name="b">The second token to compare, or null. </param>
        /// <returns>
        /// true if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, false.
        /// </returns>
        public static bool operator ==(Token<TValue> a, Token<TValue> b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return EqualityComparer<TValue>.Default.Equals(a.TokenValue, b.TokenValue) &&
                a.TokenType == b.TokenType &&
                a.Index == b.Index;
        }

        /// <summary>
        /// Determines whether two specified tokens have different values.
        /// </summary>
        /// <param name="a">The first token to compare, or null. </param>
        /// <param name="b">The second token to compare, or null. </param>
        /// <returns>
        /// true if the value of <paramref name="a"/> is different from the value of <paramref name="b"/>; otherwise, false.
        /// </returns>
        public static bool operator !=(Token<TValue> a, Token<TValue> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.TokenType, this.TokenValue);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this == obj as Token<TValue>;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Token<TValue> other)
        {
            return this == other;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<TValue>.Default.GetHashCode(this.TokenValue);
                hashCode = (hashCode * 397) ^ this.TokenType.GetHashCode();
                if (this.Index != null)
                {
                    hashCode = (hashCode * 397) ^ this.Index.Value;
                }

                return hashCode;
            }
        }
    }
}