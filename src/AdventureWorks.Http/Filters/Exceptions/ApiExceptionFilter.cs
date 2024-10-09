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
                problemDetails = MapProblemDetails(StatusCodes.Status404NotFound, "Product not found", context);
                context.Result = new NotFoundObjectResult(problemDetails);
            }
            else if(context.Exception is BadRequestException)
            {
                problemDetails = MapProblemDetails(StatusCodes.Status400BadRequest, "Bad request", context);
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else 
            {
                problemDetails = MapProblemDetails(StatusCodes.Status500InternalServerError, "Internal server error", context, true);
                context.Result = new ObjectResult(problemDetails) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            context.ExceptionHandled = true;
        }

        private ProblemDetails MapProblemDetails(int? status, string title, ExceptionContext exceptionContext, bool internalServerError = false)
        {
            var detail = exceptionContext.Exception.Message;
            if(internalServerError)
            {
                detail = "Internal server error occured when executing request. Contact support personell.";
            }

            var problemDetails = new ProblemDetails
            {
                Detail = detail,
                Status = status,
                Instance = exceptionContext.HttpContext.Request.Path,
                Title = title
            };

            return problemDetails;
        }
    }
}
