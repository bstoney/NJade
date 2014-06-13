namespace NJade
{
    using System;

    /// <summary>
    /// Defines the NJadeException class.
    /// </summary>
    public class NJadeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NJadeException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NJadeException(string message)
            : base(message)
        {
        }
    }
}