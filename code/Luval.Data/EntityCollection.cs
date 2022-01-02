using Luval.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityCollection{TEntity, TKey}"/> on a collection
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public abstract class EntityCollection<TEntity, TKey> : IEntityCollection<TEntity, TKey>
    {
        private readonly List<EntityItem> _internal = new List<EntityItem>();
        
        private class EntityItem { public TEntity Entity { get; set; } public EntityState State { get; set; } }

        /// <inheritdoc/>
        public abstract IQuery<TEntity, TKey> Query { get; }

        /// <inheritdoc/>
        public int Count { get { return _internal.Count; } }

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(TEntity item)
        {
            _internal.Add(new EntityItem() { Entity = item, State = EntityState.New });
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _internal.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(TEntity item)
        {
            return _internal.Select(i => i.Entity).Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _internal.Select(i => i.Entity).ToList().CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAdded()
        {
            return _internal.Where(i => i.State == EntityState.New).Select(i => i.Entity);
        }

        /// <inheritdoc/>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return _internal.Where(i => i.State != EntityState.Deleted).Select(i => i.Entity).GetEnumerator();
        }
        /// <inheritdoc/>
        public IEnumerable<TEntity> GetModified()
        {
            return _internal.Where(i => i.State == EntityState.Modified).Select(i => i.Entity);
        }
        /// <inheritdoc/>
        public IEnumerable<TEntity> GetRemoved()
        {
            return _internal.Where(i => i.State == EntityState.Deleted).Select(i => i.Entity);
        }
        /// <inheritdoc/>
        public bool Remove(TEntity item)
        {
            _internal.Add(new EntityItem() { Entity = item, State = EntityState.Deleted });
            return true;
        }
        /// <inheritdoc/>
        public void Update(TEntity item)
        {
            _internal.Add(new EntityItem() { Entity = item, State = EntityState.Modified });
        }
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <inheritdoc/>
        public IEntityCollection ToEntityCollection()
        {
            return new EntityCollection(GetAdded().Cast<object>(), GetModified().Cast<object>(), GetRemoved().Cast<object>());
        }
    }

    /// <summary>
    /// Represents an implementation of <see cref="IEntityCollection"/> on a collection
    /// </summary>
    public class EntityCollection : IEntityCollection
    {
        internal EntityCollection(IEnumerable<object> added, IEnumerable<object> modified, IEnumerable<object> removed)
        {
            Added = added; Modified = modified; Removed = removed;
        }

        /// <inheritdoc/>
        public IEnumerable<object> Added { get; private set; }
        /// <inheritdoc/>
        public IEnumerable<object> Modified { get; private set; }
        /// <inheritdoc/>
        public IEnumerable<object> Removed { get; private set; }
    }
}
