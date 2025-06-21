using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateWeeklyScheduleCommand from a resource 
/// </summary>
public static class CreateWeeklyScheduleCommandFromResourceAssembler
{
    /// <summary>
    /// Create a CreateWeeklyScheduleCommand from a resource 
    /// </summary>
    /// <param name="resource">The CreateWeeklyScheduleResource</param>
    /// <returns>The CreateWeeklyScheduleCommand</returns>
    public static CreateWeeklyScheduleCommand ToCommandFromResource(CreateWeeklyScheduleResource resource)
    {
        return new CreateWeeklyScheduleCommand(resource.Name);
    }
}