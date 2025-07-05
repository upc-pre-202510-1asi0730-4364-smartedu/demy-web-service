using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Provides an extension method to register the RequestAuthorizationMiddleware in the ASP.NET Core pipeline.
/// </summary>
public static class RequestAuthorizationMiddlewareExtensions
{
    /// <summary>
    /// Registers the RequestAuthorizationMiddleware in the application's middleware pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder with the middleware registered.</returns>
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}