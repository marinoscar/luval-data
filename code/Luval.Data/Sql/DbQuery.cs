using Luval.Data.Extensions;
using Luval.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Represents a query to a <see cref="Database"/> entity
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public class DbQuery<TEntity, TKey> : EntityQuery<TEntity, TKey>
    {
        /// <summary>
        /// Creates a new instance of <see cref="DbQuery{TEntity, TKey}"/>
        /// </summary>
        /// <param name="database">The target <see cref="Database"/> to query to</param>
        /// <param name="sqlDialectProvider">The <see cref="IDbDialectProvider"/> to generate the sql commands</param>
        public DbQuery(Database database, IDbDialectProvider sqlDialectProvider)
        {
            Database = database;
            Schema = DbTableSchema.Create(typeof(TEntity));
            Provider = sqlDialectProvider;
            PrimaryKey = Schema.Columns.Single(i => i.IsPrimaryKey);
        }

        #region Property Implementation

        protected Database Database { get; private set; }
        protected DbTableSchema Schema { get; private set; }
        protected IDbDialectProvider Provider { get; private set; }
        protected DbColumnSchema PrimaryKey { get; private set; }

        #endregion

        #region Implementation

        /// <inheritdoc/>
        public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Get(whereExpression, 0);
        }

        /// <inheritdoc/>
        public override TEntity Get(TKey key, EntityLoadMode mode)
        {
            var record = EntityMapper.ToDataRecord(CreateByKey(key));
            var entity = Database.ExecuteToEntityList<TEntity>(Provider.GetReadCommand(record)).SingleOrDefault();
            if (mode == EntityLoadMode.Lazy) return entity;
            foreach (var tableRef in Schema.References)
            {
                var prop = typeof(TEntity).GetProperty(tableRef.SourceColumn.PropertyName);

                var propertyValue = tableRef.IsChild ?
                    ((IEnumerable)GetChildReference(tableRef, Schema, record)).ToList(tableRef.EntityType)
                    : GetParentReference(tableRef, Schema, record).FirstOrDefault();

                prop.SetValue(entity, propertyValue);
            }
            return entity;
        }

        /// <inheritdoc/>
        public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression, int take)
        {
            var takeExpression = "";
            if (take > 0)
                takeExpression = string.Format("TOP({0})", take);
            var columns = string.Join(",", Schema.Columns.Select(i => string.Format("[{0}]", i.ColumnName)));
            var expressionProvider = new SqlExpressionProvider<TEntity>();
            var whereStatement = expressionProvider.ResolveWhere<TEntity>(whereExpression);
            var sql = string.Format("SELECT {0} {1} FROM {2} WHERE {3}", takeExpression, columns, Schema.TableName.GetFullTableName(), whereStatement);
            return Database.ExecuteToEntityList<TEntity>(sql);
        }

        /// <inheritdoc/>
        public override IEnumerable<TEntity> Get(IQueryCommand queryCommand)
        {
            return Database.ExecuteToEntityList<TEntity>(queryCommand.Get<string>());
        }

        /// <inheritdoc/>
        public override IEnumerable<IDictionary<string, object>> GetRaw(IQueryCommand queryCommand)
        {
            return Database.ExecuteToDictionaryList(queryCommand.Get<string>());
        }

        #endregion

        #region Helper Methods

        private List<object> GetParentReference(TableReference tableRef, DbTableSchema parentTable, IDataRecord record)
        {
            var parentPkName = parentTable.Columns.Single(i => i.IsPrimaryKey).ColumnName;
            var sql = string.Format("SELECT * FROM {0} WHERE {1} = {2}",
                tableRef.ReferenceTable.TableName.GetFullTableName(),
                parentPkName,
                record[tableRef.ReferenceTableKey].ToSql());
            return Database.ExecuteToEntityList(sql, CommandType.Text, null, tableRef.EntityType);
        }

        private List<object> GetChildReference(TableReference tableRef, DbTableSchema parentTable, IDataRecord record)
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1} = {2}",
                tableRef.ReferenceTable.TableName.GetFullTableName(),
                tableRef.ReferenceTableKey,
                record[parentTable.Columns.Single(i => i.IsPrimaryKey).ColumnName].ToSql());
            return Database.ExecuteToEntityList(sql, CommandType.Text, null, tableRef.EntityType);
        }

        protected virtual IDataRecord CreateByKey(object key)
        {
            return new DictionaryDataRecord(new Dictionary<string, object>() { { PrimaryKey.ColumnName, key } });
        }

        #endregion
    }
}
