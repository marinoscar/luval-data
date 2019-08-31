using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    public class EntityAdapter<T>
    {
        public Database Database { get; private set; }
        public T Entity { get; private set; }

        public int Insert()
        {
            return 0;
        }

        public int Update()
        {
            return 0;
        }

        public int Delete()
        {
            return 0;
        }


    }
}
