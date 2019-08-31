using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace luval.data
{
    public class EntityAdapter<T>
    {
        public EntityAdapter(Database database, ISqlDialectProvider<T> dialectProvider)
        {
            Entity = dialectProvider.Entity;
            Database = database;
            DialectProvider = dialectProvider;
        }

        public ISqlDialectProvider<T> DialectProvider { get; private set; }
        public Database Database { get; private set; }
        public T Entity { get; private set; }

        public int Insert()
        {
            return Database.ExecuteNonQuery(DialectProvider.GetCreateCommand());
        }

        public int Update()
        {
            return Database.ExecuteNonQuery(DialectProvider.GetUpdateCommand());
        }

        public int Delete()
        {
            return Database.ExecuteNonQuery(DialectProvider.GetDeleteCommand());
        }

        public T Read()
        {
            return Database.ExecuteToEntityList<T>(DialectProvider.GetReadCommand()).SingleOrDefault();
        }


    }
}
