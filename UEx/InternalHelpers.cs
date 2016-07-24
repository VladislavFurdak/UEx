using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Internal
{
    internal static class InternalHelpers
    {
       public static IDictionary<TJoinMemberType, IList<T>> GroupByExpression<T, TJoinMemberType>(this IEnumerable<T> sequence, Expression<Func<T, TJoinMemberType>> sourceField)
        {
           return
               sequence
               .AsQueryable()
               .GroupBy(sourceField)
               .ToDictionary(
                   key => key.Key, 
                   value =>(IList<T>)value.ToList()
                  );
        }
    }
}
