using System;
using System.ComponentModel.DataAnnotations;

namespace MQS.WebApi.Models
{
	public class AuthorModel
	{
		public Guid Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
	}
}
