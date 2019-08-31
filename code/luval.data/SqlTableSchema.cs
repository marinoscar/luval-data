using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    public class SqlTableSchema
    {
        public string TableName { get; set; }
        public string DataSchema { get; set; }
        public List<SqlColumnSchema> Columns { get; set; }
    }
}
