using System;
using Microsoft.AspNetCore.Builder;

namespace WildOasis.API.Middleware;

public static class ApiExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
    {
        var options = new ApiExceptionOptions();
        return builder.UseMiddleware<ExceptionMiddleware>(options);
    }

    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
        Action<ApiExceptionOptions> configureOptions)
    {
        var options = new ApiExceptionOptions();
        configureOptions(options);

        return builder.UseMiddleware<ExceptionMiddleware>(options);
    }
}