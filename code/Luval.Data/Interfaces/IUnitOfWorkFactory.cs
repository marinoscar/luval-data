using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an implementation of a factory to create <see cref="IUnitOfWork{TEntity, TKey}"/>
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance of a <see cref="IUnitOfWork{TEntity, TKey}"/>
        /// </summary>
        /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
        /// <typeparam name="TKey">The <see cref="Type"/> for the entity Id</typeparam>
        /// <returns>A <see cref="IUnitOfWork{TEntity, TKey}"/> object</returns>
        IUnitOfWork<TEntity, TKey> Create<TEntity, TKey>();
    }
}
