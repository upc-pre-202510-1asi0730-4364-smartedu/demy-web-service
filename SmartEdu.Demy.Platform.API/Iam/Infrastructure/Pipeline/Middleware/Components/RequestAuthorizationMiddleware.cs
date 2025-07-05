using SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Middleware component responsible for authenticating requests using JWT.
///     Validates the token, retrieves the user account, and sets it in the HTTP context.
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Invoked for each HTTP request to enforce authentication.
    ///     Skips validation if [AllowAnonymous] is detected.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="userAccountQueryService">Service to retrieve user accounts.</param>
    /// <param name="tokenService">Service to validate and parse JWT tokens.</param>
    public async Task InvokeAsync(
        HttpContext context,
        IUserAccountQueryService userAccountQueryService,
        ITokenService tokenService)
    {
        // Check if the endpoint has [AllowAnonymous] and skip if so
        var allowAnonymous = context.GetEndpoint()?.Metadata
            .Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        // Extract token from Authorization header
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        var token = authHeader?.Split(" ").Last();

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing or invalid Authorization header.");
            return;
        }

        // Validate the token and extract userId
        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid or expired token.");
            return;
        }

        // Retrieve the UserAccount from the database
        var getUserByIdQuery = new GetUserAccountByIdQuery(userId.Value);
        var user = await userAccountQueryService.Handle(getUserByIdQuery);

        if (user == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("User not found.");
            return;
        }

        // Inject the user into HttpContext.Items
        context.Items["User"] = user;

        // Continue the pipeline
        await next(context);
    }
}