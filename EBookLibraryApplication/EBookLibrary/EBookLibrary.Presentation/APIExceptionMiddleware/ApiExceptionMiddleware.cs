using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.Commons.Exceptions;
using EBookLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.APIExceptionMiddleWare
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        /// <summary>
        /// ExceptionMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex.Message);
                await ConvertException(ex, context);
            }
        }

        private async Task ConvertException(Exception exception, HttpContext context)
        {
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        
        switch (exception)
        {
            case BadRequestException accessViolationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;

            case AccessViolationException accessViolationException:
                httpStatusCode = HttpStatusCode.Forbidden;
                break;
            case NotFoundException notFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException unauthorizedAccess:
                httpStatusCode = HttpStatusCode.Unauthorized;
                break;
            default:
                httpStatusCode = HttpStatusCode.InternalServerError;
                break;

        }
            context.Response.StatusCode = (int)httpStatusCode;
            var response = _env.IsDevelopment()
                    ? new Response<string>(context.Response.StatusCode, exception.Message, exception.StackTrace?.ToString()).ToString()
                    : new Response<string>(context.Response.StatusCode, exception.Message).ToString();

            await context.Response.WriteAsync(response);
        }
    }
}
