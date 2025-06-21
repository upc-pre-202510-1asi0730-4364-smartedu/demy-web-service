namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for registering a new admin user
/// </summary>
public record SignUpAdminResource(
    string FullName,
    string Email,
    string Password
   
);