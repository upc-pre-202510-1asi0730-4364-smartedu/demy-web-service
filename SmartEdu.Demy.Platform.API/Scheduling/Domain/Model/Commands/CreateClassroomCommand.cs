namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record CreateClassroomCommand(string Code, int Capacity, string Campus);