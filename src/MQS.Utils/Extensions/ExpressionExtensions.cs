using LinqKit;
using MQS.Utils.Common;
using System;
using System.Linq.Expressions;

namespace MQS.Utils.Extensions
{
	public static class ExpressionExtensions
	{
		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
		{
			Guard.NotNull(expression, nameof(expression));

			var negated = Expression.Not(expression.Body);
			return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
		}

        public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.And(predicate) : query;
        }

        public static Expression<Func<T, bool>> AndIfNotEmpty<T>(this Expression<Func<T, bool>> query, string value, Expression<Func<T, bool>> predicate)
        {
            return query.AndIf(!string.IsNullOrWhiteSpace(value), predicate);
        }

        public static Expression<Func<T, bool>> AndIfNotNull<T>(this Expression<Func<T, bool>> query, object value, Expression<Func<T, bool>> predicate)
        {
            return query.AndIf(value != null, predicate);
        }

        public static Expression<Func<T, bool>> AndIf<T>(this ExpressionStarter<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.And(predicate) : (Expression<Func<T, bool>>)query;
        }

        public static Expression<Func<T, bool>> AndIfNotEmpty<T>(this ExpressionStarter<T> query, string value, Expression<Func<T, bool>> predicate)
        {
            return query.AndIf(!string.IsNullOrWhiteSpace(value), predicate);
        }
    }
}
