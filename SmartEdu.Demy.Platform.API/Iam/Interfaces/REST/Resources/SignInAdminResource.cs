namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for admin login
/// </summary>
public record SignInAdminResource(
    string Email,
    string Password
);