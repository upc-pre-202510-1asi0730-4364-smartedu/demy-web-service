namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to request the creation of a new teacher account.
/// </summary>
/// <param name="FullName">The full name of the teacher.</param>
/// <param name="Email">The email address of the teacher.</param>
/// <param name="Password">The plain text password that will be hashed and stored securely.</param>
public record CreateTeacherCommand(string FullName, string Email, string Password);
