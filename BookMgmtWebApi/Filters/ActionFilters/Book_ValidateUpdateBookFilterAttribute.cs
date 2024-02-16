using BookMgmtWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMgmtWebApi.Filters.ActionFilters
{
    public class Book_ValidateUpdateBookFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            var book = context.ActionArguments["book"] as Book;

            if (id.HasValue && book != null && id != book.BookId)
            {
                context.ModelState.AddModelError("BookId", "BookId is not the same as id.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
