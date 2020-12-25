using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeatherAlert_DB
{
    public static class GraphExpressionBuilder
    {

        private static Expression<Func<T, object>> BuildSelectorExpression<T>(string propertyName)
        {
            var paramExpression = Expression.Parameter(typeof(T));
            var propertyExpression = Expression.Property(paramExpression, typeof(T), propertyName);
            var castExpression = Expression.Convert(propertyExpression, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(castExpression, paramExpression);

            return lambda;
        }

        private static Func<T, object> CompileSelectorExpression<T>(string propertyName)
        {
            return BuildSelectorExpression<T>(propertyName).Compile();
        }

        public static IEnumerable<IGrouping<object, TElement>> GroupBy<TElement>(this IEnumerable<TElement> enumerable, string propertyName)
        {
            return enumerable.GroupBy(CompileSelectorExpression<TElement>(propertyName));
        }
    } 
}
