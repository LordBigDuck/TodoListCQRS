using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using TodoListAzure.Api.Models;

namespace TodoListAzure.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                var body = new ErrorResponse();
                switch (ex)
                {
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        body.Errors.Add(ex.Message);
                        body.StackTrace = ex.StackTrace;
                        break;
                }

                var result = JsonSerializer.Serialize(body);
                await response.WriteAsync(result);
            }
        }
    }
}
