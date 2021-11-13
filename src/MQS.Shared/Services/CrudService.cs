using MongoDB.Driver;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Shared.Services
{
	public abstract class CrudService<TDocument> : ICrudService<TDocument> where TDocument : IDocument
	{
		protected readonly IApplicationDbContext _context;
		protected readonly IMongoCollection<TDocument> _entitySet;

		protected CrudService(IApplicationDbContext context)
		{
			_context = context;
			_entitySet = _context.Set<TDocument>();
		}

		public long Count(Expression<Func<TDocument, bool>> filterExpression = null)
		{
			if (filterExpression == null)
				filterExpression = doc => true;

			return _entitySet.CountDocuments(filterExpression);
		}

		public async Task<long> CountAsync(Expression<Func<TDocument, bool>> filterExpression = null)
		{
			if (filterExpression == null)
				filterExpression = doc => true;

			return await _entitySet.CountDocumentsAsync(filterExpression);
		}

		public virtual long DeleteAll(Expression<Func<TDocument, bool>> predicate)
		{
			var result = _entitySet.DeleteMany(predicate);
			return result.IsAcknowledged ? result.DeletedCount : 0;
		}

		public virtual async Task<long> DeleteAllAsync(Expression<Func<TDocument, bool>> predicate)
		{
			var result = await _entitySet.DeleteManyAsync(predicate);
			return result.IsAcknowledged ? result.DeletedCount : 0;
		}

		public virtual void DeleteById(Guid id)
		{
			_entitySet.FindOneAndDelete(CreateIdFilter(id));
		}

		public virtual async Task DeleteByIdAsync(Guid id)
		{
			await _entitySet.FindOneAndDeleteAsync(CreateIdFilter(id));
		}

		public virtual void DeleteOne(Expression<Func<TDocument, bool>> predicate)
		{
			_entitySet.FindOneAndDelete(predicate);
		}

		public virtual async Task DeleteOneAsync(Expression<Func<TDocument, bool>> predicate)
		{
			await _entitySet.FindOneAndDeleteAsync(predicate);
		}

		public virtual IEnumerable<TDocument> Filter(Expression<Func<TDocument, bool>> filterExpression)
		{
			return _entitySet.Find(filterExpression).ToEnumerable();
		}

		public virtual async Task<IEnumerable<TDocument>> FilterAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return await _entitySet.Find(filterExpression).ToListAsync();
		}

		public virtual IEnumerable<TDocument> Filter(IDocumentQuery<TDocument> query)
		{
			var filterExpr = query.BuildFilterExpression();
			return _entitySet.Find(filterExpr).ToEnumerable();
		}

		public virtual async Task<IEnumerable<TDocument>> FilterAsync(IDocumentQuery<TDocument> query)
		{
			var filterExpr = query.BuildFilterExpression();
			return await _entitySet.Find(filterExpr).ToListAsync();
		}

		public virtual IEnumerable<TProjected> Filter<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectExpression)
		{
			return _entitySet.Find(filterExpression).Project(projectExpression).ToEnumerable();
		}

		public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
		{
			return _entitySet.Find(filterExpression).FirstOrDefault();
		}

		public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return await _entitySet.Find(filterExpression).FirstOrDefaultAsync();
		}

		public virtual TDocument GetById(Guid id)
		{
			return _entitySet.Find(CreateIdFilter(id)).SingleOrDefault();
		}

		public virtual async Task<TDocument> GetByIdAsync(Guid id)
		{
			return await _entitySet.Find(CreateIdFilter(id)).SingleOrDefaultAsync();
		}

		public virtual void Insert(TDocument document)
		{
			document.CreatedAt = DateTime.Now;
			_entitySet.InsertOne(document);
		}

		public virtual async Task InsertAsync(TDocument document)
		{
			document.CreatedAt = DateTime.Now;
			await _entitySet.InsertOneAsync(document);
		}

		public virtual void InsertRange(IEnumerable<TDocument> documents)
		{
			foreach (var item in documents)
			{
				item.CreatedAt = DateTime.Now;
			}
			_entitySet.InsertMany(documents);
		}

		public virtual async Task InsertRangeAsync(IEnumerable<TDocument> documents)
		{
			foreach (var item in documents)
			{
				item.CreatedAt = DateTime.Now;
			}
			await _entitySet.InsertManyAsync(documents);
		}

		public virtual void Update(TDocument document)
		{
			_entitySet.FindOneAndReplace(CreateIdFilter(document.Id), document);
		}

		public virtual async Task UpdateAsync(TDocument document)
		{
			await _entitySet.FindOneAndReplaceAsync(CreateIdFilter(document.Id), document);
		}

		protected virtual FilterDefinition<TDocument> CreateIdFilter(Guid id)
		{
			return Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
		}

	}
}
