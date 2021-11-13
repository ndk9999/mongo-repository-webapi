using MongoDB.Bson.Serialization;
using MQS.Domain.Common;
using MQS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Data.BsonMaps
{
	public class BookMap : IDocumentConfiguration
	{
		public void Configure()
		{
			BsonClassMap.RegisterClassMap<Book>(map =>
			{
				map.AutoMap();
				map.SetIgnoreExtraElements(true);
				map.MapIdMember(x => x.Id);
				map.MapMember(x => x.Title).SetIsRequired(true);
			});
		}
	}
}
