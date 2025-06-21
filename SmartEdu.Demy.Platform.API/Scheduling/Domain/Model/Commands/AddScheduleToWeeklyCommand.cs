using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record AddScheduleToWeeklyCommand(
    int WeeklyScheduleId,
    string DayOfWeek,
    string StartTime,
    string EndTime,
    int CourseId,
    int ClassroomId);