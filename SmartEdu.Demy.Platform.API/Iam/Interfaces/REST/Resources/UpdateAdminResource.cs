namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating admin profile information
/// </summary>
public record UpdateAdminResource(
    string FullName,
    string Email
);