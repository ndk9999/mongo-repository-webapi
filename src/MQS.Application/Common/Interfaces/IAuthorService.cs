using MQS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Application.Common.Interfaces
{
	public interface IAuthorService : ICrudService<Author>
	{
		Task<List<Author>> GetAllAsync();
	}
}
