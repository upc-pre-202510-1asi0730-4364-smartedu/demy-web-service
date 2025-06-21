namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating teacher profile information
/// </summary>
public record UpdateTeacherResource(
    string FullName,
    string Email,
    string NewPassword
);