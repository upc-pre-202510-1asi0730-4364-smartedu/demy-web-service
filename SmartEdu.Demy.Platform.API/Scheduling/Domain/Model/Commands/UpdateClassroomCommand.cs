namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record UpdateClassroomCommand(int Id, string Code, int Capacity, string Campus);