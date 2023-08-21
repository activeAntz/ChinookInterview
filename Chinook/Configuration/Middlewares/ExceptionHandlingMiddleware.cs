using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

namespace Chinook.Configuration.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

                if (ex.GetType() == typeof(NotImplementedException))
                {
                    statusCode = HttpStatusCode.NotImplemented;
                }
                else if (ex.GetType() == typeof(NullReferenceException))
                {
                    statusCode = HttpStatusCode.LengthRequired;
                }
                else if (ex.GetType() == typeof(OutOfMemoryException))
                {
                    statusCode = HttpStatusCode.UnsupportedMediaType;
                }
                else if (ex.GetType() == typeof(OverflowException))
                {
                    statusCode = HttpStatusCode.RequestEntityTooLarge;
                }
                else if (ex.GetType() == typeof(StackOverflowException))
                {
                    statusCode = HttpStatusCode.RequestEntityTooLarge;
                }
                else if (ex.GetType() == typeof(TypeInitializationException))
                {
                    statusCode = HttpStatusCode.NoContent;
                }
                else if (ex.GetType() == typeof(BadHttpRequestException))
                {
                    statusCode = HttpStatusCode.BadRequest;
                }
                else if (ex.GetType() == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                else if (ex.GetType() == typeof(InvalidOperationException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                }

                ExceptionProvider exceptionProvider = new ExceptionProvider((int)statusCode, ex.Message);

                context.Response.StatusCode = (int)statusCode;
                Log.Error(ex, ex.Message);
                await context.Response.WriteAsync(exceptionProvider.ToString());
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
