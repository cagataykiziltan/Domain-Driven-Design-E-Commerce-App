using Ecommence.Infrastructure.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Ecommerce.UI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IEnumerable<Type> _badRequestTypes = new[] { typeof(ValidationException), typeof(ArgumentException), typeof(ArgumentNullException) };

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment environment)
        {
            try
            {

                await _next(context);


            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, environment);
            }
        }

        private Task HandleExceptionAsync(HttpContext context,
                                         Exception exception,
                                         IWebHostEnvironment environment)
        {

            var isDevelopment = environment.IsDevelopment();
            var exceptionType = exception.GetType();
            var status = HttpStatusCode.InternalServerError;


            context.Response.ContentType = MediaTypeNames.Application.Json;

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Forbidden;

            }
            else if (_badRequestTypes.Contains(exceptionType))
            {
                status = HttpStatusCode.BadRequest;

            }

            context.Response.StatusCode = (int)status;

            var response = new HttpResponseObjectError<object>
            {
                Message = exception.Message,
                Status = (int)status
            };


            //LOG ERRORS
            var stackTrace = exception.StackTrace ?? string.Empty;
            var result = new { responseMessage = response, stackTrace }.AsJson();
            return context.Response.WriteAsync(result);


        }
    
}
}
