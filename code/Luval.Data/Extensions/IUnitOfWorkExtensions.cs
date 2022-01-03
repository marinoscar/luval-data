using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.Data.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IUnitOfWork{TEntity, TKey}"/> interface
    /// </summary>
    public static class IUnitOfWorkExtensions
    {
        /// <summary>
        /// Adds and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static Task<int> AddAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Add(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }


        /// <summary>
        /// Adds and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="userId">The id of the user adding the entity</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static Task<int> AddAndSaveAuditEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, string userId, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            entity.CreatedByUserId = userId;
            entity.UtcCreatedOn = DateTime.UtcNow;
            entity.UpdatedByUserId = userId;
            entity.UtcUpdatedOn = DateTime.UtcNow;
            return AddAndSaveAsync(uow, entity, cancellationToken);
        }

        /// <summary>
        /// Adds and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="userId">The id of the user adding the entity</param>
        /// <returns>The number of affected records</returns>
        public static int AddAndSaveAuditEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, string userId) where TEntity : IAuditableEntity<TKey>
        {
            return AddAndSaveAuditEntityAsync(uow, entity, userId, CancellationToken.None).Result;
        }

        /// <summary>
        /// Adds and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <returns>The number of affected records</returns>
        public static int AddAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return AddAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }

        /// <summary>
        /// Updates and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <returns>The number of affected records</returns>
        public static int UpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return UpdateAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }

        /// <summary>
        /// Updates and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="userId">The id of the user adding the entity</param>
        /// <returns>The number of affected records</returns>
        public static int UpdateAndSaveAuditEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, string userId) where TEntity : IAuditableEntity<TKey>
        {
            return UpdateAndSaveAuditEntityAsync(uow, entity, userId, CancellationToken.None).Result;
        }

        /// <summary>
        /// Updates and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static Task<int> UpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Update(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates and persists the provided value
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="userId">The id of the user adding the entity</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static Task<int> UpdateAndSaveAuditEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, string userId, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            entity.UpdatedByUserId = userId;
            entity.UtcUpdatedOn = DateTime.UtcNow;
            return AddAndSaveAsync(uow, entity, cancellationToken);
        }

        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by invoking the <paramref name="entityCondition"/> 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="entityCondition">A function used to identify if the entity already exists</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static async Task<int> AddOrUpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> entityCondition, CancellationToken cancellationToken)
        {
            var item = await uow.Entities.Query.GetAsync(entityCondition, cancellationToken);
            if(item != null && item.Any())
                uow.Entities.Update(entity);
            else
                uow.Entities.Add(entity);

            return await uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by looking on the entity Id property 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static async Task<int> AddOrUpdateAndSaveIdEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken) where TEntity : IIdBasedEntity<TKey>
        {
            var item = await uow.Entities.Query.GetAsync(i => i.Id != null && i.Id.Equals(entity.Id), cancellationToken);
            if (item != null && !item.Any())
                uow.Entities.Update(entity);
            else
                uow.Entities.Add(entity);

            return await uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by invoking the <paramref name="entityCondition"/> 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="entityCondition">A function used to identify if the entity already exists</param>
        /// <param name="userId">The id of the user that executed the action</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static async Task<int> AddOrUpdateAndSaveAuditEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> entityCondition, string userId, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            var item = await uow.Entities.Query.GetAsync(entityCondition, cancellationToken);
            if (item != null && item.Any())
            {
                entity.Id = item.First().Id;
                entity.UtcUpdatedOn = DateTime.UtcNow;
                entity.UpdatedByUserId = userId;
                uow.Entities.Update(entity);
            }
            else
            {
                entity.UtcCreatedOn = DateTime.UtcNow;
                entity.UtcUpdatedOn = entity.UtcCreatedOn;
                entity.CreatedByUserId = userId;
                entity.UpdatedByUserId = userId;
                uow.Entities.Add(entity);
            }
            return await uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by invoking the <paramref name="entityCondition"/> 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="entityCondition">A function used to identify if the entity already exists</param>
        /// <param name="userId">The id of the user that executed the action</param>
        /// <returns>The number of affected records</returns>
        public static int AddOrUpdateAndSaveAuditEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> entityCondition, string userId) where TEntity : IAuditableEntity<TKey>
        {
            return AddOrUpdateAndSaveAuditEntityAsync(uow, entity, entityCondition, userId, CancellationToken.None).Result;
        }


        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by invoking the <paramref name="entityCondition"/> 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="entityCondition">A function used to identify if the entity already exists</param>
        /// <returns>The number of affected records</returns>
        public static int AddOrUpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> entityCondition)
        {
            return AddOrUpdateAndSaveAsync(uow, entity, entityCondition, CancellationToken.None).Result;
        }

        /// <summary>
        /// Provides a simple implementation in which the method checks if a record exists by looking on the entity Id property 
        /// function if a value us found the <seealso cref="UpdateAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is called
        /// otherwise the <seealso cref="AddAndSaveAsync{TEntity, TKey}(IUnitOfWork{TEntity, TKey}, TEntity, CancellationToken)"/> method is executed
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <returns>The number of affected records</returns>
        public static int AddOrUpdateAndSaveIdEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity) where TEntity : IIdBasedEntity<TKey>
        {
            return AddOrUpdateAndSaveIdEntityAsync(uow, entity, CancellationToken.None).Result;
        }

        /// <summary>
        /// Remove the entity from the collection and deletes the record
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        public static Task<int> RemoveAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Remove(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Remove the entity from the collection and deletes the record
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Type"/> for the target entity</typeparam>
        /// <typeparam name="TKey"><see cref="Type" /> for the target entity Id property</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork{TEntity, TKey}"/> implementation</param>
        /// <param name="entity">An instance of the entity to use</param>
        /// <returns>The number of affected records</returns>
        public static int RemoveAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return RemoveAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }

    }
}
