namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record WeeklyScheduleResource(
    int Id,
    string Name,
    IEnumerable<ScheduleResource> Schedules,
    bool HasConflicts);