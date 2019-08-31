using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace luval.data
{
    public class SqlServerDialectProvider : ISqlDialectProvider
    {

        public SqlServerDialectProvider(object entity): this(entity, SqlTableSchema.Load(entity.GetType()))
        {

        }

        public SqlServerDialectProvider(object entity, SqlTableSchema schema)
        {
            Schema = schema;
            Entity = entity;
        }

        public SqlTableSchema Schema { get; private set; }
        public object Entity { get; private set; }

        public string GetCreateCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("INSERT ({0}) INTO {1} VALUES ({2});",
                string.Join(", ", GetSqlFormattedColumnNames((i) => !i.IsIdentity)),
                GetSqlFormattedTableName(),
                string.Join(", ", GetSqlInserValues()));
            return sw.ToString();
        }

        public string GetDeleteCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("DELETE FROM {0} WHERE {1};", GetSqlFormattedTableName(),
                string.Join(" AND ", GetKeyWhereStatement()));
            return sw.ToString();
        }

        public string GetUpdateCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("UPDATE {0} SET {1} WHERE {2};", GetSqlFormattedTableName(),
                string.Join(", ", GetUpdateValueStatement()),
                string.Join(" AND ", GetKeyWhereStatement()));
            return sw.ToString();
        }

        public string GetReadCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("SELECT {0} FROM {1} WHERE {2};",
                string.Join(", ", GetSqlFormattedColumnNames((i) => true)),
                GetSqlFormattedTableName(),
                string.Join(" AND ", GetKeyWhereStatement()));
            return sw.ToString();
        }

        private IEnumerable<string> GetUpdateValueStatement()
        {
            return GetColumnValuePair(i => !i.Item1.IsPrimaryKey && i.Item3 != null);
        }

        private IEnumerable<string> GetKeyWhereStatement()
        {
            if (!Schema.Columns.Any(i => i.IsPrimaryKey))
                throw new InvalidDataException("At least one primary key column is required");
            return GetColumnValuePair(i => i.Item1.IsPrimaryKey && i.Item3 != null);
        }

        private IEnumerable<string> GetColumnValuePair(Func<Tuple<SqlColumnSchema, object, PropertyInfo>, bool> predicate)
        {
            return GetEntityValues().Where(predicate)
                .Select(i =>
                {
                    var res = string.Format("{0} = {1}", GetSqlFormattedColumnName(i.Item1), i.Item2.ToSql());
                    if (i.Item2.IsNullOrDbNull()) res = string.Format("{0} IS NULL", GetSqlFormattedColumnName(i.Item1));
                    return res;
                }).ToList();
        }

        private IEnumerable<string> GetSqlInserValues()
        {
            return GetEntityValues().Where(i => !i.Item1.IsIdentity && i.Item2 != null)
                .Select(i => i.Item2.ToSql()).ToList();
        }

        private IEnumerable<Tuple<SqlColumnSchema, object, PropertyInfo>> GetEntityValues()
        {
            var res = new List<Tuple<SqlColumnSchema, object, PropertyInfo>>();
            foreach (var col in Schema.Columns)
            {
                var obj = default(object);
                var prop = Database.GetEntityPropertyFromFieldName(col.Name, Entity.GetType());
                if (prop != null)
                    obj = prop.GetValue(Entity);
                res.Add(new Tuple<SqlColumnSchema, object, PropertyInfo>(col, obj, prop));
            }
            return res;
        }

        private string GetSqlFormattedTableName()
        {
            return string.Format("[{0}]", Schema.TableName);
        }

        private string GetSqlFormattedColumnName(SqlColumnSchema columnSchema)
        {
            return string.Format("[{0}]", columnSchema.Name);
        }

        private IEnumerable<string> GetSqlFormattedColumnNames(Func<SqlColumnSchema, bool> predicate)
        {
            return Schema.Columns.Where(predicate).Select(GetSqlFormattedColumnName);
        }
    }
}
