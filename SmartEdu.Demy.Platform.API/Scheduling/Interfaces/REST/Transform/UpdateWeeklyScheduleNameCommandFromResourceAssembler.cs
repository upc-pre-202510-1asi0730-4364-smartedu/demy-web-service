using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an UpdateWeeklyScheduleNameCommand from a resource 
/// </summary>
public static class UpdateWeeklyScheduleNameCommandFromResourceAssembler
{
    /// <summary>
    /// Create an UpdateWeeklyScheduleNameCommand from a resource 
    /// </summary>
    /// <param name="weeklyScheduleId">The weekly schedule ID</param>
    /// <param name="resource">The UpdateWeeklyScheduleNameResource</param>
    /// <returns>The UpdateWeeklyScheduleNameCommand</returns>
    public static UpdateWeeklyScheduleNameCommand ToCommandFromResource(int weeklyScheduleId, UpdateWeeklyScheduleNameResource resource)
    {
        return new UpdateWeeklyScheduleNameCommand(weeklyScheduleId, resource.Name);
    }
}