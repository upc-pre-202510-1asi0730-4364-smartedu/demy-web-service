namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to request a password reset for a user account.
/// </summary>
/// <param name="Email">The email address associated with the user account.</param>
/// <param name="NewPassword">The new plain text password to be hashed and stored.</param>
public record ResetPasswordCommand(string Email, string NewPassword);
