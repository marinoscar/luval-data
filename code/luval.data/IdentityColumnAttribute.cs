using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    /// <summary>
    /// Specifies that a column is an identity auto increment
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityColumnAttribute : Attribute
    {
    }
}
