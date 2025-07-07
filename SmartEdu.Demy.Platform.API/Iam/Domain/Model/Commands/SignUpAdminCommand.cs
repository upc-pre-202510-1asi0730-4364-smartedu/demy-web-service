namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to register a new administrator account.
/// </summary>
/// <param name="FullName">The full name of the administrator.</param>
/// <param name="Email">The email address to associate with the new admin account.</param>
/// <param name="Password">The plain text password to be hashed and stored securely.</param>
public record SignUpAdminCommand(string FullName, string Email, string Password);

