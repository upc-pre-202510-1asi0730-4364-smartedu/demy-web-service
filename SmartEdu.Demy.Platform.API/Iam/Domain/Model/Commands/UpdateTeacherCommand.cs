namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

public record UpdateTeacherCommand(long Id, string FullName, string Email, string NewPassword);
