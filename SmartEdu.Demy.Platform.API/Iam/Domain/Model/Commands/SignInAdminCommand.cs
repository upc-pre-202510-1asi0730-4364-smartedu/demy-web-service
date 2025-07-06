namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to authenticate an admin user by email and password.
/// </summary>
/// <param name="Email">The email address of the admin user.</param>
/// <param name="Password">The plain text password used for authentication.</param>
public record SignInAdminCommand(string Email, string Password);

