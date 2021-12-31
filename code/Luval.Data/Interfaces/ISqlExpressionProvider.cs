using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Indentifies an implementation of sql expressions 
    /// </summary>
    public interface ISqlExpressionProvider
    {
        /// <summary>
        /// Resolves a where sql command
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="Type"/> of entity to resolve</typeparam>
        /// <param name="expression">The expression to resolve for the entity type</param>
        /// <returns>A <see cref="string"/> with a valid sql command</returns>
        string ResolveWhere<TEntity>(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Resolves a order by sql command
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="Type"/> of entity to resolve</typeparam>
        /// <param name="orderBy">The expression to resolve for the entity type</param>
        /// <param name="descending">Indicates if the order is descending</param>
        /// <returns>A <see cref="string"/> with a valid sql command</returns>
        string ResolveOrderBy<TEntity>(Expression<Func<TEntity, object>> orderBy, bool @descending);
    }
}
