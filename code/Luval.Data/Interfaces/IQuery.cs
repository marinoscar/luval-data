using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies the implementation of a data store query object
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public interface IQuery<TEntity, TKey>
    {
        /// <summary>
        /// Gets a single entity based on the provided key value
        /// </summary>
        /// <param name="key">The entity key</param>
        /// <returns>A <see cref="TEntity"/> object</returns>
        TEntity Get(TKey key);
        /// <summary>
        /// Gets a single entity based on the provided key value
        /// </summary>
        /// <param name="key">The entity key</param>
        /// <param name="mode">The mode of loading as specified in <see cref="EntityLoadMode"/></param>
        /// <returns>A <see cref="TEntity"/> object</returns>
        TEntity Get(TKey key, EntityLoadMode mode);
        /// <summary>
        /// Gets a single entity based on the provided key value
        /// </summary>
        /// <param name="key">The entity key</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use in the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken);
        /// <summary>
        /// Gets a single entity based on the provided key value
        /// </summary>
        /// <param name="key">The entity key</param>
        /// <param name="mode">The mode of loading as specified in <see cref="EntityLoadMode"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use in the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<TEntity> GetAsync(TKey key, EntityLoadMode mode, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="whereExpression">The <see cref="Expression{TDelegate}"/> to use to query the entities</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with the entities</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="whereExpression">The <see cref="Expression{TDelegate}"/> to use to query the entities</param>
        /// <param name="take">The number of entities to return from the data set</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with the entities</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression, int take);
        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="whereExpression">The <see cref="Expression{TDelegate}"/> to use to query the entities</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken);
        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="whereExpression">The <see cref="Expression{TDelegate}"/> to use to query the entities</param>
        /// <param name="take">The number of entities to return from the data set</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereExpression, int take, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="queryCommand">The <see cref="IQueryCommand"/> to use to get the entities</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with the entities</returns>
        IEnumerable<TEntity> Get(IQueryCommand queryCommand);

        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="queryCommand">The <see cref="IQueryCommand"/> to use to get the entities</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use in the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<IEnumerable<TEntity>> GetAsync(IQueryCommand queryCommand, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="queryCommand">The <see cref="IQueryCommand"/> to use to get the entities</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with <see cref="IDictionary{TKey, TValue}"/> with the corresponding values</returns>
        IEnumerable<IDictionary<string, object>> GetRaw(IQueryCommand queryCommand);

        /// <summary>
        /// Gets the entities resulting of the provided expression
        /// </summary>
        /// <param name="queryCommand">The <see cref="IQueryCommand"/> to use to get the entities</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use in the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation result</returns>
        Task<IEnumerable<IDictionary<string, object>>> GetRawAsync(IQueryCommand queryCommand, CancellationToken cancellationToken);

    }
}
