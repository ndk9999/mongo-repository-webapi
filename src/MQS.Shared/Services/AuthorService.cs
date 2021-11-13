using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQS.Shared.Services
{
	public class AuthorService : CrudService<Author>, IAuthorService
	{
		public AuthorService(IApplicationDbContext context) : base(context)
		{
		}

		public async Task<List<Author>> GetAllAsync()
		{
			return await _entitySet.AsQueryable()
				.OrderBy(x => x.LastName)
				.ThenBy(x => x.FirstName)
				.ToListAsync();
		}
	}
}
