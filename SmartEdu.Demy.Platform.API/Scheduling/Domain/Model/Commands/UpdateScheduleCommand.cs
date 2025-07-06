namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record UpdateScheduleCommand(
    int ScheduleId,
    int ClassroomId,
    string StartTime,
    string EndTime,
    string DayOfWeek
);