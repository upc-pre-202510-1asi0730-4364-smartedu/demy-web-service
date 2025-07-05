using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Custom authorization attribute that ensures a signed-in user is present in the HTTP context.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    ///     Checks if the user is authorized to access the resource.
    ///     Skips authorization if the action is marked with [AllowAnonymous].
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Skip authorization if [AllowAnonymous] is applied
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            Console.WriteLine("Authorization skipped: AllowAnonymous attribute found.");
            return;
        }

        // Check if user was set in HttpContext.Items
        var user = context.HttpContext.Items["User"] as UserAccount;

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}