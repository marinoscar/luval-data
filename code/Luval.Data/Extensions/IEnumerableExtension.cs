using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luval.Data.Extensions
{
    /// <summary>
    /// Provide extension methods to the <see cref="IEnumerable"/> interface
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Caste the values of the <see cref="IEnumerable"/> implementation
        /// </summary>
        /// <param name="ie">An instance of the <see cref="IEnumerable"/> implementation</param>
        /// <param name="innerType">The <see cref="Type"/> to cast</param>
        /// <returns>An casted <see cref="IEnumerable"/> implemenation</returns>
        public static IEnumerable Cast(this IEnumerable ie, Type innerType)
        {
            var methodInfo = typeof(Enumerable).GetMethod("Cast");
            var genericMethod = methodInfo.MakeGenericMethod(innerType);
            return genericMethod.Invoke(null, new[] { ie }) as IEnumerable;
        }

        /// <summary>
        /// Caste the values of the <see cref="IEnumerable"/> implementation
        /// </summary>
        /// <param name="ie">An instance of the <see cref="IEnumerable"/> implementation</param>
        /// <param name="innerType">The <see cref="Type"/> to cast</param>
        /// <returns>An casted <see cref="IList{T}"/> implemenation</returns>
        public static object ToList(this IEnumerable self, Type innerType)
        {
            var methodInfo = typeof(Enumerable).GetMethod("ToList");
            var genericMethod = methodInfo.MakeGenericMethod(innerType);
            var enumerable = Cast(self, innerType);
            var res = genericMethod.Invoke(null, new[] { enumerable });
            return res;
        }
    }
}
