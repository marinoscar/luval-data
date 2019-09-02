using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Luval.Data.tests.Mocks.Data
{
    public class MConnection : IDbConnection
    {
        public MConnection()
        {
            State = ConnectionState.Closed;
            Database = "master";
        }

        public List<IDataRecord> Records { get; set; }

        public string ConnectionString { get; set; }

        public int ConnectionTimeout { get; set; }

        public string Database { get; private set; }

        public ConnectionState State { get; private set; }

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new MTransaction() { Connection = this, IsolationLevel = il };
        }

        public void ChangeDatabase(string databaseName)
        {
            Database = databaseName;
        }

        public void Close()
        {
            State = ConnectionState.Closed;
        }

        public IDbCommand CreateCommand()
        {
            return new MCommand() { Connection = this, Records = this.Records };
        }

        public void Dispose()
        {
        }

        public void Open()
        {
            State = ConnectionState.Open;
        }
    }
}
