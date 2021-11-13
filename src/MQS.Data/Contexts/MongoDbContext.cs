using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Settings;
using MQS.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQS.Data.Contexts
{
	public class MongoDbContext : IApplicationDbContext
	{
		private readonly MongoClient _client;
		private readonly IMongoDatabase _database;
		private readonly Dictionary<Type, string> _collectionNames;

		public MongoDbContext(IOptions<MongoDbSettings> dbSettings)
		{
			_client = new MongoClient(dbSettings.Value.ConnectionString);
			_database = _client.GetDatabase(dbSettings.Value.DatabaseName);
			_collectionNames = new Dictionary<Type, string>();
		}

		public IMongoCollection<T> Set<T>()
		{
			var name = GetCollectionName<T>();
			return _database.GetCollection<T>(name);
		}

		public async Task<IClientSessionHandle> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			var session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
			session.StartTransaction();

			return session;
		}

		private string GetCollectionName<T>()
		{
			var documentType = typeof(T);

			if (!_collectionNames.ContainsKey(documentType))
			{
				var collectionAttribute = documentType
					.GetCustomAttributes(typeof(MongoCollectionAttribute), true)
					.FirstOrDefault() as MongoCollectionAttribute;

				_collectionNames[documentType] = collectionAttribute?.CollectionName ?? documentType.Name;
			}

			return _collectionNames[documentType];
		}
	}
}
