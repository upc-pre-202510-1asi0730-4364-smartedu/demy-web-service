using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
/// Middleware for globally handling unhandled exceptions in the HTTP request pipeline.
/// Converts exceptions into consistent JSON error responses.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance.</param>
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware, catching unhandled exceptions and returning JSON error responses.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var status = exception switch
        {
            ValidationException         => HttpStatusCode.BadRequest,
            KeyNotFoundException        => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            SecurityTokenException      => HttpStatusCode.Unauthorized,
            InvalidOperationException   => HttpStatusCode.Conflict,
            DbUpdateException           => HttpStatusCode.Conflict,
            _                           => HttpStatusCode.InternalServerError
        };

        var responseObject = new
        {
            timestamp = DateTime.UtcNow,
            status    = (int)status,
            error     = status.ToString(),
            message   = exception.Message,
            path      = context.Request.Path.ToString()
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = (int)status;
        return context.Response.WriteAsync(JsonSerializer.Serialize(responseObject));
    }
}