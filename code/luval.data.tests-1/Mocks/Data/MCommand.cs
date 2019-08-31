using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace luval.data.tests.Mocks.Data
{
    public class MCommand : IDbCommand
    {
        public List<IDataRecord> Records { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDbConnection Connection { get; set; }

        public IDataParameterCollection Parameters { get; private set; }

        public IDbTransaction Transaction { get; set; }
        public UpdateRowSource UpdatedRowSource { get; set; }

        public void Cancel()
        {
        }

        public IDbDataParameter CreateParameter()
        {
            return new MDataParameter();
        }

        public void Dispose()
        {
        }

        public int ExecuteNonQuery()
        {
            return 0;
        }

        public IDataReader ExecuteReader()
        {
            return ExecuteReader(CommandBehavior.Default);
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return new MDataReader(Records);
        }

        public object ExecuteScalar()
        {
            return null;
        }

        public void Prepare()
        {
        }
    }
}
