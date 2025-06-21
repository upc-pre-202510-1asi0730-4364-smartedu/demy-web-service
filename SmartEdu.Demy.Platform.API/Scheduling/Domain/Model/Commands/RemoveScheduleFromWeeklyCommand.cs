namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record RemoveScheduleFromWeeklyCommand(
    int WeeklyScheduleId,
    int ScheduleId); 