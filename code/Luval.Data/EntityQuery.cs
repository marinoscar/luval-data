using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.Data
{

    /// <summary>
    /// Represents a query to a <see cref="Database"/> entity
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public abstract class EntityQuery<TEntity, TKey> : IQuery<TEntity, TKey>
    {
        /// <inheritdoc/>
        public TEntity Get(TKey key)
        {
            return Get(key, EntityLoadMode.Lazy);
        }

        /// <inheritdoc/>
        public abstract TEntity Get(TKey key, EntityLoadMode mode);

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression);
        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression, int take);

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return Get(whereExpression); }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereExpression, int take, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return Get(whereExpression, take); }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken)
        {
            return GetAsync(key, EntityLoadMode.Lazy, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetAsync(TKey key, EntityLoadMode mode, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return Get(key, mode); }, cancellationToken);
        }

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> Get(IQueryCommand queryCommand);


        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetAsync(IQueryCommand queryCommand, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return Get(queryCommand); }, cancellationToken);
        }

        /// <inheritdoc/>
        public abstract IEnumerable<IDictionary<string, object>> GetRaw(IQueryCommand queryCommand);

        /// <inheritdoc/>
        public Task<IEnumerable<IDictionary<string, object>>> GetRawAsync(IQueryCommand queryCommand, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return GetRaw(queryCommand); }, cancellationToken);
        }
    }
}
