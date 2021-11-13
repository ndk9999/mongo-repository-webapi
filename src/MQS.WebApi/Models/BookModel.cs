using MQS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MQS.WebApi.Models
{
	public class BookModel
	{
		public Guid Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public string Introduction { get; set; }

		public decimal Price { get; set; }

		[Required]
		public string Publisher { get; set; }

		[Required]
		public Guid AuthorId { get; set; }

		public Picture CoverImage { get; set; }
	}
}
