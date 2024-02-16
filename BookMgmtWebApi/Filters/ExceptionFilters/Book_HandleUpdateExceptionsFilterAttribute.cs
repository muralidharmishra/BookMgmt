using BookMgmtWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMgmtWebApi.Filters.ExceptionFilters
{
    public class Book_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strBookId = context.RouteData.Values["id"] as string;
            if(int.TryParse(strBookId, out var bookId))
            {
                if (!BookRepository.BookExists(bookId))
                {
                    context.ModelState.AddModelError("BookId", "Book doesn't exist anymore.");
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
