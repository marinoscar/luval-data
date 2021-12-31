using Luval.Data.Attributes;
using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Entities
{
    /// <summary>
    /// An <see langword="abstract"/> implementation of the <see cref="IAuditableEntity{TKey}"/> interface
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> for the <seealso cref="Id"/> property</typeparam>
    public abstract class AuditEntity<TKey> : IAuditableEntity<TKey>
    {

        /// <summary>
        /// Creates a new instance of <see cref="AuditEntity"/>
        /// </summary>
        public AuditEntity()
        {
            UtcCreatedOn = DateTime.UtcNow;
            UtcUpdatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the Id value
        /// </summary>
        [PrimaryKey]
        public TKey Id { get ; set; }
        /// <summary>
        /// Gets or sets the UtcCreatedOn value, defaulted to <seealso cref="DateTime.UtcNow"/>
        /// </summary>
        public DateTime UtcCreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the Id of the user id that created the record
        /// </summary>
        public string CreatedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the UtcUpdatedOn value, defaulted to <seealso cref="DateTime.UtcNow"/>
        /// </summary>
        public DateTime UtcUpdatedOn { get; set; }
        /// <summary>
        /// Gets or sets the value of the user id that updated the record
        /// </summary>
        public string UpdatedByUserId { get; set; }
    }
}
