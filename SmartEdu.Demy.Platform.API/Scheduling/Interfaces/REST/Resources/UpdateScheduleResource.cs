namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record UpdateScheduleResource(
    int ClassroomId,
    string StartTime,
    string EndTime,
    string DayOfWeek
    );