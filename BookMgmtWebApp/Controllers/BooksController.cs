using BookMgmtWebApp.Data;
using BookMgmtWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMgmtWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public BooksController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Book>>("books"));
        }

        public IActionResult CreateBook(int id) 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("books", book);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch(WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
                
            }

            return View(book);
        }

        public async Task<IActionResult> UpdateBook(int bookId)
        {
            try
            {
                var book = await webApiExecuter.InvokeGet<Book>($"books/{bookId}");
                if (book != null)
                {
                    return View(book);
                }
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"books/{book.BookId}", book);
                    return RedirectToAction(nameof(Index));
                }
                catch(WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
            }

            return View(book);
        }

        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                await webApiExecuter.InvokeDelete($"books/{bookId}");
                return RedirectToAction(nameof(Index));
            }
            catch(WebApiException ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index), await webApiExecuter.InvokeGet<List<Book>>("books"));
            }
            
        }

        private void HandleWebApiException(WebApiException ex)
        {
            if (ex.ErrorResponse != null &&
                        ex.ErrorResponse.Errors != null &&
                        ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
                }
            }
        }
    }
}
