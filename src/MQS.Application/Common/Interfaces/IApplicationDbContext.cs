using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace MQS.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		IMongoCollection<T> Set<T>();

		Task<IClientSessionHandle> BeginTransactionAsync(CancellationToken cancellationToken = default);
	}
}
