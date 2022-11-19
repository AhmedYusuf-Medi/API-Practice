using Api.Models.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services.Common;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Academy.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                

                var result = new Result<Exception> { Message = error?.Message, ExceptionSource = error.Source};
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }

    //Extension method used to add the middleware to the HTTP request pipeline.
    public static class GeneralExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
