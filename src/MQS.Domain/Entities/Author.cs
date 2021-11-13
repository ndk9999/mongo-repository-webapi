using MQS.Domain.Common;
using MQS.Utils.Attributes;
using System;

namespace MQS.Domain.Entities
{
	[MongoCollection("authors")]
	public class Author : Document
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }


		public Author()
		{
			Id = Guid.NewGuid();
		}
	}
}
