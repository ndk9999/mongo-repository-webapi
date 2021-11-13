using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MQS.WebApi.Extensions;
using System.Threading.Tasks;

namespace MQS.WebApi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			await host.InitAsync();
			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
