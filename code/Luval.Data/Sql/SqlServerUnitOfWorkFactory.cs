using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Provides a Sql Server implementation for the <see cref="IUnitOfWorkFactory"/>
    /// </summary>
    public class SqlServerUnitOfWorkFactory : IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="SqlServerUnitOfWorkFactory"/>
        /// </summary>
        /// <param name="sqlServerConnectionString">The connection string to use</param>
        public SqlServerUnitOfWorkFactory(string sqlServerConnectionString) : this(sqlServerConnectionString, new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="SqlServerUnitOfWorkFactory"/>
        /// </summary>
        /// <param name="sqlServerConnectionString">The connection string to use</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to use to create the <see cref="IUnitOfWork{TEntity, TKey}"/> sql server objects</param>
        public SqlServerUnitOfWorkFactory(string sqlServerConnectionString, IDataRecordMapper dataRecordMapper)
        {
            Database = new SqlServerDatabase(sqlServerConnectionString, dataRecordMapper);
            ProviderFactory = new SqlServerDialectFactory();
        }

        /// <summary>
        /// Gets the <see cref="Luval.Data.Sql.Database"/> to use
        /// </summary>
        public Database Database { get; private set; }
        protected DbDialectProvider ProviderFactory { get; private set; }

        /// <inheritdoc/>
        public IUnitOfWork<TEntity, TKey> Create<TEntity, TKey>()
        {
            return new DbUnitOfWork<TEntity, TKey>(Database, ProviderFactory.Create(DbTableSchema.Create(typeof(TEntity))));
        }
    }
}
