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
    public static class IUnitOfWorkExtensions
    {
        public static Task<int> AddAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Add(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        public static Task<int> UpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Update(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        public static async Task<int> AddOrUpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition, CancellationToken cancellationToken)
        {
            var item = await uow.Entities.Query.GetAsync(recordCondition, cancellationToken);
            if(item != null && !item.Any())
                uow.Entities.Update(entity);
            else
                uow.Entities.Add(entity);

            return await uow.SaveChangesAsync(cancellationToken);
        }

        public static async Task<int> AddOrUpdateAndSaveIdEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken) where TEntity : IIdBasedEntity<TKey>
        {
            var item = await uow.Entities.Query.GetAsync(i => i.Id != null && i.Id.Equals(entity.Id), cancellationToken);
            if (item != null && !item.Any())
                uow.Entities.Update(entity);
            else
                uow.Entities.Add(entity);

            return await uow.SaveChangesAsync(cancellationToken);
        }

        public static Task<int> AddOrUpdateAndSaveAuditEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            return AddOrUpdateAndSaveAuditEntityAsync(uow, entity, recordCondition, null, cancellationToken);
        }

        public static async Task<int> AddOrUpdateAndSaveAuditEntityAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition, string userId, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            var item = await uow.Entities.Query.GetAsync(recordCondition, cancellationToken);
            if (item != null && !item.Any())
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


        public static int AddOrUpdateAndSaveAuditEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition, string userId, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            return AddOrUpdateAndSaveAuditEntityAsync(uow, entity, recordCondition, userId, CancellationToken.None).Result;
        }

        public static int AddOrUpdateAndSaveAuditEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition, CancellationToken cancellationToken) where TEntity : IAuditableEntity<TKey>
        {
            return AddOrUpdateAndSaveAuditEntityAsync(uow, entity, recordCondition, null, CancellationToken.None).Result;
        }

        public static int AddOrUpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition)
        {
            return AddOrUpdateAndSaveAsync(uow, entity, recordCondition, CancellationToken.None).Result;
        }

        public static int AddOrUpdateAndSaveIdEntity<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity) where TEntity : IIdBasedEntity<TKey>
        {
            return AddOrUpdateAndSaveIdEntityAsync(uow, entity, CancellationToken.None).Result;
        }

        public static Task<int> RemoveAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, CancellationToken cancellationToken)
        {
            uow.Entities.Remove(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        public static int AddAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return AddAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }

        public static int UpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return UpdateAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }

        public static int RemoveAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity)
        {
            return RemoveAndSaveAsync(uow, entity, CancellationToken.None).Result;
        }
    }
}
