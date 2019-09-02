using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Luval.Data
{
    public class EntityAdapter
    {
        public EntityAdapter(Database database, ISqlDialectProvider dialectProvider)
        {
            Database = database;
            DialectProvider = dialectProvider;
        }

        public ISqlDialectProvider DialectProvider { get; private set; }
        public Database Database { get; private set; }

        public int Insert(IDataRecord record)
        {
            return Database.ExecuteNonQuery(DialectProvider.GetCreateCommand(record));
        }

        public int Update(IDataRecord record)
        {
            return Database.ExecuteNonQuery(DialectProvider.GetUpdateCommand(record));
        }

        public int Delete(IDataRecord record)
        {
            return Database.ExecuteNonQuery(DialectProvider.GetDeleteCommand(record));
        }

        public T Read<T>(IDataRecord record)
        {
            return Database.ExecuteToEntityList<T>(DialectProvider.GetReadCommand(record)).SingleOrDefault();
        }

        public int ExecuteInTransaction(IEnumerable<DataRecordAction> records)
        {
            var count = 0;
            Database.WithCommand((cmd) => {
                cmd.CommandTimeout = cmd.Connection.ConnectionTimeout;
                foreach(var record in records)
                {
                    switch (record.Action)
                    {
                        case DataAction.Insert:
                            cmd.CommandText = DialectProvider.GetCreateCommand(record.Record);
                            count += cmd.ExecuteNonQuery();
                            break;
                        case DataAction.Update:
                            cmd.CommandText = DialectProvider.GetUpdateCommand(record.Record);
                            count += cmd.ExecuteNonQuery();
                            break;
                        case DataAction.Delete:
                            cmd.CommandText = DialectProvider.GetDeleteCommand(record.Record);
                            count += cmd.ExecuteNonQuery();
                            break;
                    }
                }
            });
            return count;
        }


    }
}
