namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for teacher login
/// </summary>
public record SignInTeacherResource(
    string Email,
    string Password
);