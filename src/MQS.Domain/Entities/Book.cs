using MQS.Domain.Common;
using MQS.Utils.Attributes;
using System;

namespace MQS.Domain.Entities
{
	[MongoCollection("books")]
	public class Book : Document
	{
		public string Title { get; set; }

		public string Category { get; set; }

		public string Introduction { get; set; }

		public decimal Price { get; set; }

		public string Publisher { get; set; }

		public Guid AuthorId { get; set; }

		public Picture CoverImage { get; set; }


		public Book()
		{
			Id = Guid.NewGuid();
			CoverImage = new Picture();
		}
	}
}
