namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to request the deletion of a teacher account.
/// </summary>
/// <param name="Id">The unique identifier of the teacher to be deleted.</param>
public record DeleteTeacherCommand(long Id);
