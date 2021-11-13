using MQS.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Utils.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, int pageIndex, int pageSize)
		{
			Guard.PagingArgsValid(pageIndex, pageSize, nameof(pageIndex), nameof(pageSize));

			if (pageIndex == 0 && pageSize == int.MaxValue)
			{
				return source;
			}
			else
			{
				var skip = pageIndex * pageSize;
				return skip == 0 ? source.Take(pageSize) : source.Skip(skip).Take(pageSize);
			}
		}

		public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		{
			return condition ? query.Where(predicate) : query;
		}

		public static IQueryable<T> Transform<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IQueryable<T>> transform)
		{
			return condition ? transform(source) : source;
		}
	}
}
