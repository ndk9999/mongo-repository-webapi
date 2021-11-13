using LinqKit;
using Microsoft.AspNetCore.Mvc;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using MQS.Utils.Extensions;
using System;
using System.Linq.Expressions;

namespace MQS.WebApi.Models
{
	[BindProperties(SupportsGet = true)]
	public class BookQuery : IDocumentQuery<Book>
	{
		[BindProperty(Name = "t")]
		public string Title { get; set; }

		[BindProperty(Name = "c")]
		public string Category { get; set; }

		[BindProperty(Name = "p")]
		public string Publisher { get; set; }


		public Expression<Func<Book, bool>> BuildFilterExpression(IApplicationDbContext context)
		{
			return PredicateBuilder.New<Book>(true)
				.AndIfNotEmpty(Title, b => b.Title.Contains(Title))
				.AndIfNotEmpty(Category, b => b.Category == Category)
				.AndIfNotEmpty(Publisher, b => b.Publisher == Publisher);
		}
	}
}
