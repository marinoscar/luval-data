using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace luval.data.tests.Mocks.Data
{
    public class MTransaction : IDbTransaction
    {
        public IDbConnection Connection { get; internal set; }
        public IsolationLevel IsolationLevel { get; internal set; }

        public void Commit()
        {
        }

        public void Dispose()
        {
        }

        public void Rollback()
        {
        }
    }
}
