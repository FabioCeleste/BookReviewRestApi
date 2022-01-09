using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookReview.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                if (books == null) return NotFound("Books not found.");

                return Ok(books);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound("Books not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetByAuthor(string author)
        {
            try
            {
                var book = await _bookService.GetAllBooksByAuthorAsync(author);
                if (book == null) return NotFound("Books not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            try
            {
                var book = await _bookService.GetAllBooksByTitleAsync(title);
                if (book == null) return NotFound("Books not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("score/{score}")]
        public async Task<IActionResult> GetByscore(int score)
        {
            try
            {
                var book = await _bookService.GetAllBooksByScoreAsync(score);
                if (book == null) return NotFound("Books not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookDto model)
        {
            try
            {
                var book = await _bookService.AddBook(model);
                if (book == null) return BadRequest("Error to save book.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(BookDto model, int id)
        {
            try
            {
                var book = await _bookService.UpdateBook(model, id);
                if (book == null) return BadRequest("Error to update book.");

                return Ok(model);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _bookService.DeleteBook(id))
                    return Ok("Delete");
                else
                    return BadRequest("Error to delete.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}
