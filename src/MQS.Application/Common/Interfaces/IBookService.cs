using MQS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQS.Application.Common.Interfaces
{
	public interface IBookService : ICrudService<Book>
	{
		Task<List<Book>> GetAllAsync();

		Task<List<Book>> FindByAuthorAsync(Guid authorId);
	}
}
