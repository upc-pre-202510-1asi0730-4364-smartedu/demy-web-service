using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;

public record AddScheduleToWeeklyCommand(
    int WeeklyScheduleId,
    string DayOfWeek,
    TimeRange TimeRange,
    int CourseId,
    int ClassroomId,
    int TeacherId);