using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using System.Linq;
using Luval.Data.Attributes;

namespace Luval.Data
{
    /// <summary>
    /// Provides an implementation for <see cref="IDataReader"/> on a <see cref="List{IDataRecord}"/> collection
    /// </summary>
    public class ListDataReader : IDataReader
    {
        private IDataRecord _current;

        /// <summary>
        /// Creates a new instance of <see cref="ListDataReader"/>
        /// </summary>
        /// <param name="entityType">The entity <see cref="Type"/> to use</param>
        public ListDataReader(Type entityType)
        {
            EntityType = entityType;
            Records = new List<IDataRecord>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="ListDataReader"/>
        /// </summary>
        /// <param name="entityType">The entity <see cref="Type"/> to use</param>
        /// <param name="records">A <see cref="IEnumerable{IDataRecord}"/> with the data</param>
        public ListDataReader(Type entityType, IEnumerable<IDataRecord> records) : this(entityType)
        {
            Records = new List<IDataRecord>(records);
        }

        /// <inheritdoc/>
        public object this[int i] => _current[i];

        /// <inheritdoc/>
        public object this[string name] => _current[name];

        /// <inheritdoc/>
        public int Depth { get; internal set; }

        /// <inheritdoc/>
        public bool IsClosed { get; internal set; }

        /// <inheritdoc/>
        public int RecordsAffected => Records.Count;

        /// <inheritdoc/>
        public int FieldCount => _current.FieldCount;

        /// <summary>
        /// Gets the <see cref="List{IDataRecord}"/> with the records
        /// </summary>
        public List<IDataRecord> Records { get; }

        /// <summary>
        /// Gets the entity <see cref="Type"/>
        /// </summary>
        public Type EntityType { get; }

        /// <inheritdoc/>
        public void Close()
        {
            IsClosed = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Records.Clear();
            _current = null;
        }

        /// <inheritdoc/>
        public bool GetBoolean(int i)
        {
            return _current.GetBoolean(i);
        }

        /// <inheritdoc/>
        public byte GetByte(int i)
        {
            return _current.GetByte(i);
        }

        /// <inheritdoc/>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _current.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <inheritdoc/>
        public char GetChar(int i)
        {
            return _current.GetChar(i);
        }

        /// <inheritdoc/>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _current.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /// <inheritdoc/>
        public IDataReader GetData(int i)
        {
            return _current.GetData(i);
        }

        /// <inheritdoc/>
        public string GetDataTypeName(int i)
        {
            return _current.GetDataTypeName(i);
        }

        /// <inheritdoc/>
        public DateTime GetDateTime(int i)
        {
            return _current.GetDateTime(i);
        }

        /// <inheritdoc/>
        public decimal GetDecimal(int i)
        {
            return _current.GetDecimal(i);
        }

        /// <inheritdoc/>
        public double GetDouble(int i)
        {
            return _current.GetDouble(i);
        }
        /// <inheritdoc/>
        public Type GetFieldType(int i)
        {
            return _current.GetFieldType(i);
        }
        /// <inheritdoc/>
        public float GetFloat(int i)
        {
            return _current.GetFloat(i);
        }
        /// <inheritdoc/>
        public Guid GetGuid(int i)
        {
            return _current.GetGuid(i);
        }
        /// <inheritdoc/>
        public short GetInt16(int i)
        {
            return _current.GetInt16(i);
        }
        /// <inheritdoc/>
        public int GetInt32(int i)
        {
            return _current.GetInt32(i);
        }
        /// <inheritdoc/>
        public long GetInt64(int i)
        {
            return _current.GetInt64(i);
        }
        /// <inheritdoc/>
        public string GetName(int i)
        {
            return _current.GetName(i);
        }
        /// <inheritdoc/>
        public int GetOrdinal(string name)
        {
            return _current.GetOrdinal(name);
        }
        /// <inheritdoc/>
        public DataTable GetSchemaTable()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[] {
                new DataColumn("AllowDBNull", typeof(bool)),
                new DataColumn("ColumnName", typeof(string)),
                new DataColumn("IsAutoIncrement", typeof(bool)),
                new DataColumn("IsIdentity", typeof(bool)),
                new DataColumn("IsKey", typeof(bool)),
                new DataColumn("BaseTableName", typeof(string)),
            });
            foreach (var prop in EntityType.GetProperties())
            {
                if (prop.GetCustomAttribute<NotMappedAttribute>() != null) continue;
                var dr = dt.NewRow();
                var colAtt = prop.GetCustomAttribute<ColumnNameAttribute>();
                var tabAtt = EntityType.GetCustomAttribute<TableNameAttribute>();
                dr["ColumnName"] = colAtt != null ? colAtt.Name : prop.Name;
                dr["IsAutoIncrement"] = prop.GetCustomAttribute<IdentityColumnAttribute>() != null;
                dr["IsIdentity"] = dr["IsAutoIncrement"];
                dr["IsKey"] = prop.GetCustomAttribute<PrimaryKeyAttribute>() != null;
                dr["AllowDBNull"] = Nullable.GetUnderlyingType(prop.PropertyType) != null;
                dr["BaseTableName"] = tabAtt != null ? tabAtt.TableName.GetFullTableName() : EntityType.Name;
            }
            return dt;
        }
        /// <inheritdoc/>
        public string GetString(int i)
        {
            return _current.GetString(i);
        }
        /// <inheritdoc/>
        public object GetValue(int i)
        {
            return _current.GetValue(i);
        }
        /// <inheritdoc/>
        public int GetValues(object[] values)
        {
            return _current.GetValues(values);
        }
        /// <inheritdoc/>
        public bool IsDBNull(int i)
        {
            return _current.IsDBNull(i);
        }
        /// <inheritdoc/>
        public bool NextResult()
        {
            return !((Records.IndexOf(_current) + 1) > (Records.Count - 1));
        }
        /// <inheritdoc/>
        public bool Read()
        {
            if (_current == null)
            {
                _current = Records.FirstOrDefault();
                return true;
            }
            var res = NextResult();
            if (res) _current = Records[Records.IndexOf(_current) + 1];
            return res;
        }
    }
}
