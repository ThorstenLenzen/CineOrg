using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Toto.Utilities.Exceptions;

namespace Toto.CineOrg.WebApi.Hosting
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
 
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
 
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An exception was handled by the exception handling middleware.");
            
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var message = "Ooops! A problem occured. Please inform your support.";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    message = notFoundException.ToJson();
                    break;
                case InvalidParameterException invalidParameterException:
                    code = HttpStatusCode.BadRequest;
                    message = invalidParameterException.ToJson();
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(message);
        }
    }
}