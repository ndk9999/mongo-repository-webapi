using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQS.Shared.Services
{
	public class BookService : CrudService<Book>, IBookService
	{
		public BookService(IApplicationDbContext context) : base(context)
		{
		}

		public async Task<List<Book>> GetAllAsync()
		{
			return await _entitySet.AsQueryable()
				.OrderBy(x => x.Title)
				.ToListAsync();
		}

		public async Task<List<Book>> FindByAuthorAsync(Guid authorId)
		{
			var filterExpr = Builders<Book>.Filter.Eq(book => book.AuthorId, authorId);
			return await _entitySet.Find(filterExpr).ToListAsync();
		}
	}
}
