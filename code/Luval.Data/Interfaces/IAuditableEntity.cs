using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an entity that implementes <see cref="IIdBasedEntity<TKey>"/> providing values to audit the state of an entity
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> for the Id property</typeparam>
    public interface IAuditableEntity<TKey> : IIdBasedEntity<TKey>, ICreatedEntity, IUpdatedEntity
    {
    }
}
