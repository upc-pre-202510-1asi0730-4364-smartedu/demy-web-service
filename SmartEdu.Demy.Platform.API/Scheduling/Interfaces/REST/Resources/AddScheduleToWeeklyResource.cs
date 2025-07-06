namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record AddScheduleToWeeklyResource(
    string DayOfWeek,
    string StartTime,
    string EndTime,
    int CourseId,
    int ClassroomId,
    int TeacherId);