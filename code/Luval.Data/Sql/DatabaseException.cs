using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// A database originated exception
    /// </summary>
    public class DatabaseException : DbException
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public DatabaseException() : this(null, null)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="message">Message for the exception</param>
        public DatabaseException(string message) : this(message, null)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="message">Message for the exception</param>
        /// <param name="innerException">An inner <see cref="Exception"/> object</param>
        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
