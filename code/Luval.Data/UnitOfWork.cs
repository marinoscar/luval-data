using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.Data
{
    /// <summary>
    /// Provides an <see langword="abstract"/> implementation of the <see cref="IUnitOfWork{TEntity, TKey}"/> pattern
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class UnitOfWork<TEntity, TKey> : IUnitOfWork<TEntity, TKey>
    {
        /// <inheritdoc/>
        public abstract IEntityCollection<TEntity, TKey> Entities { get; }

        /// <inheritdoc/>
        public abstract int SaveChanges();

        /// <inheritdoc/>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => { return SaveChanges(); }, cancellationToken);
        }
    }
}
