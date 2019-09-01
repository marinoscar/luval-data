using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace luval.data
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


    }
}
