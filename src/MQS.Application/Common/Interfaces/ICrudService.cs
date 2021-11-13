using MQS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MQS.Application.Common.Interfaces
{
	public interface ICrudService<TDocument> where TDocument : IDocument
	{
		long Count(Expression<Func<TDocument, bool>> filterExpression = null);

		Task<long> CountAsync(Expression<Func<TDocument, bool>> filterExpression);

		IEnumerable<TDocument> Filter(Expression<Func<TDocument, bool>> filterExpression);

		Task<IEnumerable<TDocument>> FilterAsync(Expression<Func<TDocument, bool>> filterExpression);

		IEnumerable<TDocument> Filter(IDocumentQuery<TDocument> query);

		Task<IEnumerable<TDocument>> FilterAsync(IDocumentQuery<TDocument> query);

		IEnumerable<TProjected> Filter<TProjected>(
			Expression<Func<TDocument, bool>> filterExpression,
			Expression<Func<TDocument, TProjected>> projectExpression);

		TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

		Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

		TDocument GetById(Guid id);

		Task<TDocument> GetByIdAsync(Guid id);

		void Insert(TDocument document);

		Task InsertAsync(TDocument document);

		void InsertRange(IEnumerable<TDocument> documents);

		Task InsertRangeAsync(IEnumerable<TDocument> documents);

		void Update(TDocument document);

		Task UpdateAsync(TDocument document);

		void DeleteById(Guid id);

		Task DeleteByIdAsync(Guid id);

		void DeleteOne(Expression<Func<TDocument, bool>> predicate);

		Task DeleteOneAsync(Expression<Func<TDocument, bool>> predicate);

		long DeleteAll(Expression<Func<TDocument, bool>> predicate);

		Task<long> DeleteAllAsync(Expression<Func<TDocument, bool>> predicate);
	}
}
