using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    /// <summary>
    ///  Specifies the name of a table for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : NameBaseAttribute
    {
        public TableNameAttribute(string name) : base(name)
        {

        }
    }
}
