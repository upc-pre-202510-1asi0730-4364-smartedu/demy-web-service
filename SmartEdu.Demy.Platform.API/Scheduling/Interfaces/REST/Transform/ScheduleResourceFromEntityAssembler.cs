using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Schedule entity to ScheduleResource 
/// </summary>
public static class ScheduleResourceFromEntityAssembler
{
    public static ScheduleResource ToResourceFromEntity(Schedule entity)
    {
        return new ScheduleResource(
            entity.Id,
            entity.TimeRange.StartTime.ToString(),
            entity.TimeRange.EndTime.ToString(),
            entity.DayOfWeek.ToString(),
            entity.CourseId,
            entity.ClassroomId,
            entity.TeacherId);
    }
}