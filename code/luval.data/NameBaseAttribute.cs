using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    public abstract class NameBaseAttribute : Attribute
    {
        public NameBaseAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
