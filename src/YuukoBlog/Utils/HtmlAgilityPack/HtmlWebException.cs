
using System;

namespace Pomelo.HtmlAgilityPack
{
    /// <summary>
    /// Represents an exception thrown by the HtmlWeb utility class.
    /// </summary>
    public class HtmlWebException : Exception
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of the HtmlWebException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public HtmlWebException(string message)
            : base(message)
        {
        }

        #endregion
    }
}