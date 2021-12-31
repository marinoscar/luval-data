using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an implementation of a collection of entities
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/></typeparam>
    /// <typeparam name="TKey">The entity Id <see cref="Type"/></typeparam>
    public interface IEntityCollection<TEntity, TKey> : ICollection<TEntity>
    {
        /// <summary>
        /// Marks an entity in the colleciton as updated
        /// </summary>
        /// <param name="item">The entity item</param>
        void Update(TEntity item);
        /// <summary>
        /// Gets the items in the collection marked as added
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> with the items</returns>
        IEnumerable<TEntity> GetAdded();
        /// <summary>
        /// Gets the items in the collection marked as updated
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> with the items</returns>
        IEnumerable<TEntity> GetModified();
        /// <summary>
        /// Gets the items in the collection marked as removed
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> with the items</returns>
        IEnumerable<TEntity> GetRemoved();
        /// <summary>
        /// Gets the items in a <see cref="IEntityCollection"/>
        /// </summary>
        IEntityCollection ToEntityCollection();
        /// <summary>
        /// Gets the <see cref="IQuery{TEntity, TKey}"/> for the <see cref="IEntityCollection{TEntity, TKey}"/>
        /// </summary>
        IQuery<TEntity, TKey> Query { get; }

    }

    /// <summary>
    /// Indetifies an implementation of an Entity collection
    /// </summary>
    public interface IEntityCollection
    {
        /// <summary>
        /// Gets the items added
        /// </summary>
        IEnumerable<object> Added { get; }
        /// <summary>
        /// Gets the items modified
        /// </summary>
        IEnumerable<object> Modified { get; }
        /// <summary>
        /// Gets the items removed
        /// </summary>
        IEnumerable<object> Removed { get; }
    }
}
