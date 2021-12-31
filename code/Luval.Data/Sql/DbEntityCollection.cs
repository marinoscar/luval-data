using Luval.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Implementation of <see cref="IEntityCollection{TEntity, TKey}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class DbEntityCollection<TEntity, TKey> : EntityCollection<TEntity, TKey>
    {
        private IQuery<TEntity, TKey> _query;
        /// <summary>
        /// Creates a new instance of <see cref="DbEntityCollection{TEntity, TKey}"/>
        /// </summary>
        /// <param name="database">The <see cref="Database"/> object to use</param>
        /// <param name="sqlDialectProvider">The <see cref="IDbDialectProvider"/> to use</param>
        public DbEntityCollection(Database database, IDbDialectProvider sqlDialectProvider)
        {
            _query = new DbQuery<TEntity, TKey>(database, sqlDialectProvider);
        }

        /// <inheritdoc/>
        public override IQuery<TEntity, TKey> Query { get { return _query; } }

    }
}
