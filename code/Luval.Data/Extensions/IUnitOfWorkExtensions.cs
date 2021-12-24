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

        public static int AddOrUpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity, TKey> uow, TEntity entity, Expression<Func<TEntity, bool>> recordCondition)
        {
            return AddOrUpdateAndSaveAsync(uow, entity, recordCondition, CancellationToken.None).Result;
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
