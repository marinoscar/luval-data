using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    /// <summary>
    /// Specifies that a column is a primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
