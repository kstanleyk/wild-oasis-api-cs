using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WildOasis.API.Middleware;

public class ApiExceptionOptions
{
    public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
}