using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MQS.Application.Common.Interfaces;
using MQS.Data.Contexts;
using MQS.Domain.Common;
using MQS.Domain.Settings;
using System;
using System.Linq;

namespace MQS.Data
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration)
		{
			ConfigureMongoDb();

			services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
			services.AddScoped<IApplicationDbContext, MongoDbContext>();

			return services;
		}

		private static void ConfigureMongoDb()
		{
			// Configure document map
			var assembly = typeof(MongoDbContext).Assembly;
			var types = assembly
				.GetExportedTypes()
				.Where(x => x.GetInterfaces().Any(i => i.GetType() == typeof(IDocumentConfiguration)))
				.ToList();

			foreach (var mapType in types)
			{
				var instance = Activator.CreateInstance(mapType);
				var methodInfo = mapType.GetMethod("Configure");

				methodInfo?.Invoke(instance, new object[] { });
			}

			// Configure serializer
			BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

			// Conventions
			var pack = new ConventionPack()
			{
				new IgnoreExtraElementsConvention(true),
				new IgnoreIfDefaultConvention(true)
			};
			ConventionRegistry.Register("BookStore", pack, t => true);
		}
	}
}
