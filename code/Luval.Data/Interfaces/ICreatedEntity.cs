using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an entity that can described when and who created the entity record
    /// </summary>
    public interface ICreatedEntity
    {
        /// <summary>
        /// Gets or sets the UtcCreatedOn value, defaulted to <seealso cref="DateTime.UtcNow"/>
        /// </summary>
        DateTime UtcCreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the Id of the user id that created the record
        /// </summary>
        string CreatedByUserId { get; set; }

    }
}
