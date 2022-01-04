using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Represents the changes between an entity and the value of that same entity in the data store
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/></typeparam>
    public class EntityChanges<TEntity>
    {
        internal EntityChanges(TEntity store, TEntity entity, IEnumerable<EntityChange> changes)
        {
            StoreEntity = store;
            Entity = entity;
            Changes = changes;
        }
        /// <summary>
        /// Gets the entity that is currently in the data store
        /// </summary>
        public TEntity StoreEntity { get; private set; }
        /// <summary>
        /// Gets or sets the entity that is being evaluated for changes
        /// </summary>
        public TEntity Entity { get; set; }
        /// <summary>
        /// Gets a collection with the changes
        /// </summary>
        public IEnumerable<EntityChange> Changes { get; private set; }
    }
}
