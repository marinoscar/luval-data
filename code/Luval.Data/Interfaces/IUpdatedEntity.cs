using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an entity that can described when and who updated the entity record
    /// </summary>
    public interface IUpdatedEntity
    {
        /// <summary>
        /// Gets or sets the UtcUpdatedOn value, defaulted to <seealso cref="DateTime.UtcNow"/>
        /// </summary>
        DateTime UtcUpdatedOn { get; set; }
        /// <summary>
        /// Gets or sets the value of the user id that updated the record
        /// </summary>
        string UpdatedByUserId { get; set; }
    }
}
