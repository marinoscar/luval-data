using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an implementation of the Unit Of Work pattern
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
    /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
    public interface IUnitOfWork<TEntity, TKey>
    {
        /// <summary>
        /// Gets a <see cref="IEntityCollection{TEntity, TKey}"/> with the entities linked to the Unit of Work
        /// </summary>
        IEntityCollection<TEntity, TKey> Entities { get; }

        /// <summary>
        /// Perists all of the values contained in the <seealso cref="Entities"/> property to the data stored implemented
        /// </summary>
        /// <returns>The number of affected records</returns>
        int SaveChanges();
        /// <summary>
        /// Perists all of the values contained in the <seealso cref="Entities"/> property to the data stored implemented
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use in the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with an operation for the number of affected records</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
