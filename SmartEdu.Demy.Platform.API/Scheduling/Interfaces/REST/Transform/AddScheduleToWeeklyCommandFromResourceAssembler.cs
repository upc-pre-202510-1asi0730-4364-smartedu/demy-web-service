using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an AddScheduleToWeeklyCommand from a resource 
/// </summary>
public static class AddScheduleToWeeklyCommandFromResourceAssembler
{
    /// <summary>
    /// Create an AddScheduleToWeeklyCommand from a resource 
    /// </summary>
    /// <param name="weeklyScheduleId">The weekly schedule ID</param>
    /// <param name="resource">The AddScheduleToWeeklyResource</param>
    /// <returns>The AddScheduleToWeeklyCommand</returns>
    public static AddScheduleToWeeklyCommand ToCommandFromResource(int weeklyScheduleId, AddScheduleToWeeklyResource resource)
    {
        return new AddScheduleToWeeklyCommand(
            weeklyScheduleId,
            resource.DayOfWeek,
            resource.StartTime,
            resource.EndTime,
            resource.CourseId,
            resource.ClassroomId);
    }
}