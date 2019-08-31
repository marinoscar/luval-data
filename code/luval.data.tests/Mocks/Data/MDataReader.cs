using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace luval.data.tests.Mocks.Data
{
    public class MDataReader : IDataReader
    {
        private IDataRecord _current;
        private readonly List<IDataRecord> _records;

        public MDataReader()
        {
            _records = new List<IDataRecord>();
        }

        public MDataReader(IEnumerable<IDataRecord> records)
        {
            _records = new List<IDataRecord>(records);
        }

        public object this[int i] => _current[i];

        public object this[string name] => _current[name];

        public int Depth { get; internal set; }

        public bool IsClosed { get; internal set; }

        public int RecordsAffected => _records.Count;

        public int FieldCount => _current.FieldCount;

        public void Close()
        {
            IsClosed = true;
        }

        public void Dispose()
        {
        }

        public bool GetBoolean(int i)
        {
            return _current.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _current.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _current.GetBytes(i,fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _current.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _current.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return _current.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return _current.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _current.GetDateTime(i);
        }

        public decimal GetDecimal(int i)
        {
            return _current.GetDecimal(i);
        }

        public double GetDouble(int i)
        {
            return _current.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return _current.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return _current.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            return _current.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _current.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _current.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _current.GetInt64(i);
        }

        public string GetName(int i)
        {
            return _current.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return _current.GetOrdinal(name);
        }

        public DataTable GetSchemaTable()
        {
            return new DataTable();
        }

        public string GetString(int i)
        {
            return _current.GetString(i);
        }

        public object GetValue(int i)
        {
            return _current.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return _current.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return _current.IsDBNull(i);
        }

        public bool NextResult()
        {
            return !((_records.IndexOf(_current) + 1) > (_records.Count - 1));
        }

        public bool Read()
        {
            if (_current == null) {
                _current = _records.FirstOrDefault();
                return true;
            }
            var res = NextResult();
            if (res) _current = _records[_records.IndexOf(_current) + 1];
            return res;
        }
    }
}
