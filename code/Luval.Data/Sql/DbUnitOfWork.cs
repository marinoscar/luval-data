using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Luval.Data.Sql
{

    /// <summary>
    /// Provides an implementation of the Unit of Work pattern for <see cref="IDbConnection"/> interfaces
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public class DbUnitOfWork<TEntity, TKey> : UnitOfWork<TEntity, TKey>
    {
        private IEntityCollection<TEntity, TKey> _entities;

        /// <summary>
        /// Creates a new instance of <see cref="DbUnitOfWork{TEntity, TKey}"/>
        /// </summary>
        /// <param name="database">The target <see cref="Database"/> to create the implementation sql commands</param>
        /// <param name="sqlDialectProvider">The <see cref="IDbDialectProvider"/> to generate the sql commands</param>
        public DbUnitOfWork(Database database, IDbDialectProvider sqlDialectProvider)
        {
            _entities = new DbEntityCollection<TEntity, TKey>(database, sqlDialectProvider);
            Database = database;
            SqlDialectProvider = sqlDialectProvider;
        }

        /// <inheritdoc/>
        public override IEntityCollection<TEntity, TKey> Entities { get { return _entities; } }
        protected IDbDialectProvider SqlDialectProvider { get; private set; }
        protected Database Database { get; private set; }

        internal IEnumerable<string> GetCommands()
        {
            var commands = new List<string>();
            foreach (var item in Entities.GetAdded())
                commands.Add(SqlDialectProvider.GetCreateCommand(EntityMapper.ToDataRecord(item), true));

            foreach (var item in Entities.GetModified())
                commands.Add(SqlDialectProvider.GetUpdateCommand(EntityMapper.ToDataRecord(item)));

            foreach (var item in Entities.GetRemoved())
                commands.Add(SqlDialectProvider.GetDeleteCommand(EntityMapper.ToDataRecord(item)));

            return commands;
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            var commands = GetCommands();
            Entities.Clear();
            return Database.ExecuteNonQuery(string.Join(Environment.NewLine, commands));
        }
    }
}
