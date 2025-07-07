namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource to reset a user password
/// </summary>
public record ResetPasswordResource(
    string Email,
    string NewPassword
);