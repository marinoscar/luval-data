using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace luval.data
{
    /// <summary>
    /// Provides an abstraction for the schema of a sql column
    /// </summary>
    public class SqlColumnSchema
    {
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsIdentity { get; set; }

        public static SqlColumnSchema Load(PropertyInfo property)
        {
            return new SqlColumnSchema()
            {
                Name = GetColumnName(property),
                IsPrimaryKey = property.GetCustomAttribute<PrimaryKeyAttribute>() != null,
                IsIdentity = property.GetCustomAttribute<PrimaryKeyAttribute>() != null
            };
        }

        internal static string GetColumnName(PropertyInfo property)
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            if (att == null) return property.Name;
            return ((ColumnNameAttribute)att).Name;
        }
    }
}
