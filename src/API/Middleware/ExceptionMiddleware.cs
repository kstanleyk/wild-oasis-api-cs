using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Common.Exceptions;
using Core.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WildOasis.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            var validationErrors = ex.ValidationErrors;
            var response = new 
            {
                Success = false,
                ex.Message,
                ValidationErrors = validationErrors
            };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            WebHelper.LogWebError("dama-erp", "Core API", ex, context);

            var innerExMessage = GetInnermostExceptionMessage(ex);

            var response = new ApiError
            {
                Status = 500,
                Detail = _env.IsDevelopment() ? ex.StackTrace : null,
                Title =  _env.IsDevelopment() ? innerExMessage : "Some kind of error occurred in the API. Please use the id and contact our " +
                                                                "support team if the problem persists.",
                Id = Guid.NewGuid().ToString(),
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

    private static string GetInnermostExceptionMessage(Exception exception)
    {
        if (exception.InnerException != null)
            return GetInnermostExceptionMessage(exception.InnerException);

        return exception.Message;
    }
}