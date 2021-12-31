using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Attributes
{
    /// <summary>
    /// Specifies that a entity property should not be mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : Attribute
    {
    }
}
