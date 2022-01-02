using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Represents the implementation of factory to create <see cref="IUnitOfWork{TEntity, TKey}"/> from the <see cref="DbUnitOfWork{TEntity, TKey}"/> implementation
    /// </summary>
    public class DbUnitOfWorkFactory : IUnitOfWorkFactory
    {

        /// <summary>
        /// Gets the <see cref="Luval.Data.Sql.Database"/>
        /// </summary>
        public Database Database { get; private set; }
        protected DbDialectProvider ProviderFactory { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="DbUnitOfWorkFactory"/>
        /// </summary>
        /// <param name="database">The target <see cref="Database"/> to create the implementation sql commands</param>
        /// <param name="sqlDialectProvider">The <see cref="IDbDialectProvider"/> to generate the sql commands</param>
        public DbUnitOfWorkFactory(Database database, DbDialectProvider dialectProviderFactory)
        {
            Database = database; ProviderFactory = dialectProviderFactory;
        }

        /// <inheritdoc/>
        public IUnitOfWork<TEntity, TKey> Create<TEntity, TKey>()
        {
            return new DbUnitOfWork<TEntity, TKey>(Database, ProviderFactory.Create(DbTableSchema.Create(typeof(TEntity))));
        }
    }
}
