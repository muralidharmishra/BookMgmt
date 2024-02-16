using BookMgmtWebApi.Filters.ActionFilters;
using BookMgmtWebApi.Filters.ExceptionFilters;
using BookMgmtWebApi.Models;
using BookMgmtWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMgmtWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(BookRepository.GetBooks());
        }

        [HttpGet("{id}")]
        [Book_ValidateBookIdFilter]
        public IActionResult GetBookById(int id)
        {

            return Ok(BookRepository.GetBookByid(id));
        }

        [HttpPost]
        [Book_ValidateCreateBookFilter]
        public IActionResult CreateBook([FromBody] Book book)
        {

            BookRepository.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        [Book_ValidateBookIdFilter]
        [Book_ValidateUpdateBookFilter]
        [Book_HandleUpdateExceptionsFilter]
        public IActionResult UpdateBook(int id, Book book)
        {
            BookRepository.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Book_ValidateBookIdFilter]
        public IActionResult DeleteBook(int id)
        {
            var book = BookRepository.GetBookByid(id);

            BookRepository.DeleteBook(id);

            return Ok(book);
        }
    }
}
