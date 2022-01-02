using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Provides an implementation of <see cref="DbDialectProvider"/> for Sql Server databases
    /// </summary>
    public class SqlServerDialectFactory : DbDialectProvider
    {
        /// <inheritdoc/>
        public override IDbDialectProvider Create(DbTableSchema schema)
        {
            return new SqlServerDialectProvider(schema);
        }
    }
}
