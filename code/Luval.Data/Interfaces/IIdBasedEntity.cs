using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an entity with a primary key <seealso cref="Id"/> property
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> for the <seealso cref="Id"/> property</typeparam>
    public interface IIdBasedEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the value for the Id unique key
        /// </summary>
        TKey Id { get; set; }
    }
}
