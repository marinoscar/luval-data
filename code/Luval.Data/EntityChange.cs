using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Represents a change in an entity property
    /// </summary>
    public class EntityChange
    {
        internal EntityChange(PropertyInfo property, object orignal, object newVal)
        {
            Property = property;
            OriginalValue = orignal;
            NewValue = NewValue;
        }
        /// <summary>
        /// Gets the property with the change
        /// </summary>
        public PropertyInfo Property { get; private set; }
        /// <summary>
        /// Gets the original value
        /// </summary>
        public object OriginalValue { get; private set; }
        /// <summary>
        /// Gets the new value
        /// </summary>
        public object NewValue { get; private set; }
    }
}
