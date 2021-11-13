using Microsoft.AspNetCore.Mvc;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using MQS.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MQS.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;
		private readonly IBookService _bookService;

		public AuthorController(IAuthorService authorService, IBookService bookService)
		{
			_authorService = authorService;
			_bookService = bookService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Author>>> GetAll()
		{
			var authors = await _authorService.GetAllAsync();
			
			return Ok(authors);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetAuthor(Guid id)
		{
			var author = await _authorService.GetByIdAsync(id);

			if (author == null) return NotFound();

			return Ok(author);
		}

		[HttpGet("{id}/books")]
		public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBooks(Guid id)
		{
			var books = await _bookService.FindByAuthorAsync(id);
			return Ok(books);
		}

		[HttpPost]
		public async Task<ActionResult<Author>> CreateAuthor([FromBody] AuthorModel model)
		{
			if (!ModelState.IsValid) return BadRequest();

			var author = new Author()
			{
				FirstName = model.FirstName,
				LastName = model.LastName
			};
			await _authorService.InsertAsync(author);

			return CreatedAtRoute(nameof(GetAuthor), new { author.Id }, author);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Author>> UpdateAuthor(Guid id, [FromBody] AuthorModel model)
		{
			if (!id.Equals(model.Id)) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			var author = await _authorService.GetByIdAsync(id);
			if (author == null) return NotFound();

			await _authorService.UpdateAsync(author);

			return Ok(author);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(Guid id)
		{
			var author = await _authorService.GetByIdAsync(id);
			if (author == null) return NotFound();

			await _authorService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
