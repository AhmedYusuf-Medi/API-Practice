namespace CarShop.WebAPI.Helpers
{
    using CarShop.Service.Common.Exceptions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

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
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ExceptionData { Message = error?.Message, StatusCode = response.StatusCode });
                await response.WriteAsync(result);
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

    public class ExceptionData
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}