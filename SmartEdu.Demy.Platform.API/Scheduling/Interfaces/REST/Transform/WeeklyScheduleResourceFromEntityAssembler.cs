using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert WeeklySchedule entity to WeeklyScheduleResource 
/// </summary>
public static class WeeklyScheduleResourceFromEntityAssembler
{
    /// <summary>
    /// Convert WeeklySchedule entity to WeeklyScheduleResource 
    /// </summary>
    /// <param name="entity">The WeeklySchedule entity</param>
    /// <returns>The WeeklyScheduleResource</returns>
    public static WeeklyScheduleResource ToResourceFromEntity(WeeklySchedule entity)
    {
        var scheduleResources = entity.Schedules.Select(ScheduleResourceFromEntityAssembler.ToResourceFromEntity);

        return new WeeklyScheduleResource(
            entity.Id,
            entity.Name,
            scheduleResources);
    }
}