using Microsoft.AspNetCore.Mvc;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using MQS.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQS.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> GetAll([FromQuery] BookQuery query)
		{
			var books = await _bookService.FilterAsync(query);

			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook(Guid id)
		{
			var book = await _bookService.GetByIdAsync(id);

			if (book == null) return NotFound();

			return Ok(book);
		}

		[HttpPost]
		public async Task<ActionResult<Book>> CreateBook([FromBody] BookModel model)
		{
			if (!ModelState.IsValid) return BadRequest();

			var book = new Book()
			{
				Title = model.Title,
				Category = model.Category,
				Introduction = model.Introduction,
				Price = model.Price,
				Publisher = model.Publisher,
				AuthorId = model.AuthorId,
				CoverImage = model.CoverImage
			};

			await _bookService.InsertAsync(book);

			return CreatedAtRoute(nameof(GetBook), new { book.Id }, book);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Book>> UpdateBook(Guid id, [FromBody] BookModel model)
		{
			if (!id.Equals(model.Id)) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			var book = await _bookService.GetByIdAsync(id);
			if (book == null) return NotFound();

			book.Title = model.Title;
			book.Category = model.Category;
			book.Introduction = model.Introduction;
			book.Price = model.Price;
			book.Publisher = model.Publisher;
			book.AuthorId = model.AuthorId;
			book.CoverImage = model.CoverImage;

			await _bookService.UpdateAsync(book);

			return Ok(book);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(Guid id)
		{
			var book = await _bookService.GetByIdAsync(id);
			if (book == null) return NotFound();

			await _bookService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
