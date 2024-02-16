using BookMgmtWebApi.Controllers;
using BookMgmtWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMgmt.Test
{
    public class BookMgmtWebApiTest
    {
        private readonly BooksController _booksController;
        public BookMgmtWebApiTest()
        {
            _booksController = new BooksController();
        }


        [Fact]
        public void GetBooksTest()
        {
            var result = _booksController.GetBooks();

            Assert.IsType<OkObjectResult>(result);

            var list = result as OkObjectResult;

            Assert.IsType<List<Book>>(list?.Value);

        }

        [Fact]
        public void GetBookByIdTest()
        {
            var result = _booksController.GetBookById(1);
            Assert.IsType<OkObjectResult>(result); 
            
            var book = result as OkObjectResult;
            Assert.IsType<Book>(book?.Value);
        }

        [Fact]
        public void CreateBookTest()
        {
            var book = new Book()
            {
                Title = "Test",
                Author = "Test",
                Genre = "Test",
                Price = 20
            };

            var response = _booksController.CreateBook(book);

            Assert.IsType<CreatedAtActionResult>(response);

            var bookItem = response as CreatedAtActionResult;
            Assert.IsType<Book>(bookItem?.Value);
        }

        [Fact]
        public void DeleteBookTest()
        {
            var response = _booksController.DeleteBook(2);

            Assert.IsType<OkObjectResult>(response);
        }
    }
}