namespace CarShop.WebAPI.Helpers
{
    using CarShop.Models.Base;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Data.Exception;
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

        public async Task Invoke(HttpContext context, IExceptionLogService exceptionLogService)
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

                        var exceptionLog = new ExceptionLog
                        {
                            ExceptionMessage = error.Message != null ? error.Message : "There is no message!",
                            InnerException = error.InnerException != null ? error.InnerException.Message : "There is no inner exception!",       
                            StackTrace = error.StackTrace != null ? error.StackTrace : "There is no stack trace!",
                        };

                        await exceptionLogService.CreateAsync(exceptionLog);

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