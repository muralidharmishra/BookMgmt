using BookMgmtWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMgmtWebApi.Filters.ActionFilters
{
    public class Book_ValidateBookIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var bookId = context.ActionArguments["id"] as int?;
            if (bookId.HasValue)
            {
                if (bookId.Value <= 0)
                {
                    context.ModelState.AddModelError("BookId", "BookId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (!BookRepository.BookExists(bookId.Value))
                {
                    context.ModelState.AddModelError("BookId", "Book doesn't exist.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }


        }
    }
}
