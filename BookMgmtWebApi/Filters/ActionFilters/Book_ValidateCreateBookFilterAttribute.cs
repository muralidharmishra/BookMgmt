using BookMgmtWebApi.Models;
using BookMgmtWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMgmtWebApi.Filters.ActionFilters
{
    public class Book_ValidateCreateBookFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var book = context.ActionArguments["book"] as Book;
            if (book == null)
            {
                context.ModelState.AddModelError("Book", "Book object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingBook = BookRepository.GetBookByProperties(book.Title, book.Genre, book.Author);
                if (existingBook != null)
                {
                    context.ModelState.AddModelError("Book", "Book already exists.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }

        }
    }
}
