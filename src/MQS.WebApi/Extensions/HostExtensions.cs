using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQS.Data;
using System.Threading.Tasks;

namespace MQS.WebApi.Extensions
{
	public static class HostExtensions
	{
		public static async Task InitAsync(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			var serviceProvider = scope.ServiceProvider;

			await DataSeeder.SeedAsync(serviceProvider);
		}

	}
}
