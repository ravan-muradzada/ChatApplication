using ChatApplication.Domain.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApplication.WebAPI.Filters.ExceptionFilters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;
            short statusCode = 500;

            switch (context.Exception)
            {
                case InvalidAccessTokenException:
                    statusCode = 401;
                    break;
                case ObjectNotFoundException:
                    statusCode = 404;
                    break;
                case ConflictException:
                    statusCode = 409;
                    break;
                default:
                    statusCode = 500;
                    break;
            }

            context.Result = new ObjectResult(new { error = message })
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
