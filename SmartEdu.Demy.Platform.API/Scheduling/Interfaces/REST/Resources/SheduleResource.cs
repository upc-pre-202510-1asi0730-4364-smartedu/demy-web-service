namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record ScheduleResource(
    int Id,
    string StartTime,
    string EndTime,
    string DayOfWeek,
    int CourseId,
    int ClassroomId,
    long TeacherId
    );
