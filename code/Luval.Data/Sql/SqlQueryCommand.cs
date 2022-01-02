using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Represents sql command query
    /// </summary>
    public class SqlQueryCommand : IQueryCommand
    {
        private string _query;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="sqlQuery">A <see cref="string"/> with the sql command</param>
        public SqlQueryCommand(string sqlQuery)
        {
            _query = sqlQuery;
        }

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Convert.ChangeType(_query, typeof(T));
        }
    }
}
