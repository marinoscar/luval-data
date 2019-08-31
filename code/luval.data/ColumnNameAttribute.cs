﻿using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data
{
    /// <summary>
    /// Specifies the name of a column for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnNameAttribute : NameBaseAttribute
    {
        public ColumnNameAttribute(string name):base(name)
        {
        }
    }
}