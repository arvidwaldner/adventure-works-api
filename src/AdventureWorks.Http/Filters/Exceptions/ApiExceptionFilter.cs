using AdventureWorks.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdventureWorks.Http.Filters.Exceptions
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ProblemDetails problemDetails;

            if(context.Exception is NotFoundException)
            {
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Product not found",
                    Detail = context.Exception.Message,
                    Instance = context.HttpContext.Request.Path
                };

                context.Result = new NotFoundObjectResult(problemDetails);
            }
            else 
            {
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected internal server error occured",
                    Detail = context.Exception.Message,
                    Instance = context.HttpContext.Request.Path
                };

                context.Result = new ObjectResult(problemDetails)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
