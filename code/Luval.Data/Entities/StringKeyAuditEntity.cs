using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Entities
{
    /// <summary>
    /// An <see langword="abstract"/> implementation of the <see cref="AuditEntity{TKey}"/> with a Id <see cref="string"/> data type
    /// </summary>
    public abstract class StringKeyAuditEntity : AuditEntity<string>
    {
        /// <summary>
        /// Creates a new instance of <see cref="StringKeyAuditEntity"/>
        /// </summary>
        public StringKeyAuditEntity() : base()
        {
            Id = Guid.NewGuid().ToString();
            UtcCreatedOn = DateTime.UtcNow;
            UtcUpdatedOn = UtcCreatedOn;
        }
    }
}
