namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record UpdateCourseCommand(int Id, string Name, string Code, string Description);