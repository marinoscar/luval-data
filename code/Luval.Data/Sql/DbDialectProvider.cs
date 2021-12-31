using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Implementation of the <see cref="IDbDialectProvider"/>
    /// </summary>
    public abstract class DbDialectProvider
    {
        public abstract IDbDialectProvider Create(DbTableSchema schema);

        /// <summary>
        /// Creates a new instance of <see cref="IDbDialectProvider"/>
        /// </summary>
        /// <param name="entityType">The <see cref="Type"/> for the entity to use for metadata extraction</param>
        /// <returns>A new instance of <see cref="IDbDialectProvider"/></returns>
        public virtual IDbDialectProvider Create(Type entityType)
        {
            return Create(DbTableSchema.Create(entityType));
        }
    }
}
