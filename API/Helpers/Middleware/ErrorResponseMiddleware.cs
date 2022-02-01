using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Helpers.Middleware
{
    public class ErrorResponseMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if      (exception is Exception)     code = HttpStatusCode.NotFound;
           
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleDomainExceptionAsync(HttpContext context, DomainException exception)
        {
            var result = JsonConvert.SerializeObject(new { statusCode = exception.StatusCode, message = exception.Message, errors = exception.Errors });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            return context.Response.WriteAsync(result);
        }
    }
    
}