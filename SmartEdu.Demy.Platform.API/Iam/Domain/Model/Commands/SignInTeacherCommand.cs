namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to authenticate a teacher user using their email and password.
/// </summary>
/// <param name="Email">The email address of the teacher.</param>
/// <param name="Password">The plain text password used for authentication.</param>
public record SignInTeacherCommand(string Email, string Password);
