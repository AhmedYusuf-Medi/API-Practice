using Api.Models.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services.Common;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academy.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public ExceptionHandlingMiddleware(RequestDelegate next) =>
            requestDelegate = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate.Invoke(context);
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
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                
                var result = JsonSerializer.Serialize(new InfoResult { Message = error?.Message, ExceptionSource = error.Source });
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
}
