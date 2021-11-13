using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQS.Application.Common.Interfaces;
using MQS.Shared.Services;
using System;

namespace MQS.Shared
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IBookService, BookService>();

			return services;
		}
	}
}
