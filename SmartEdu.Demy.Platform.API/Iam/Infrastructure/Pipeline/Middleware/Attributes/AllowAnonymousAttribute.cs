namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Marks a controller action as allowing anonymous access, bypassing authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{

}