using Microsoft.Extensions.DependencyInjection;
using System;

namespace MQS.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			return services;
		}
	}
}
