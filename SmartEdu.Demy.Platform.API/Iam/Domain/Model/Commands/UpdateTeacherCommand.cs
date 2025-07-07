namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to update an existing teacher's account information.
/// </summary>
/// <param name="Id">The unique identifier of the teacher to update.</param>
/// <param name="FullName">The updated full name of the teacher.</param>
/// <param name="Email">The updated email address of the teacher.</param>
/// <param name="NewPassword">The new plain text password to replace the current one (optional).</param>
public record UpdateTeacherCommand(long Id, string FullName, string Email, string NewPassword);
