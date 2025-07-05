namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new teacher profile
/// </summary>
public record CreateTeacherResource(
    string Email,
    string FullName,
    string Password
);