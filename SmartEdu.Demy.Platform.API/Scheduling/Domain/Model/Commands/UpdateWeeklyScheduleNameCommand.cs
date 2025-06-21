namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record UpdateWeeklyScheduleNameCommand(
    int WeeklyScheduleId,
    string NewName); 