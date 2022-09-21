using Application.Commom.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
                await ErrorHandlerAsync(context, ex, _logger);
            }
        }

        private async Task ErrorHandlerAsync(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            object errors = null;

            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case ErrorHandler eh:
                    logger.LogError(ex, "Manejador Error");
                    errors = eh;
                    context.Response.StatusCode = (int)eh.Code;
                    break;

                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "message" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (errors != null)
            {
                var resultados = JsonConvert.SerializeObject(errors);
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
