using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public class UpdateScheduleCommandFromResourceAssembler
{
    public static UpdateScheduleCommand ToCommandFromResource(int scheduleId, UpdateScheduleResource resource)
    {
        return new UpdateScheduleCommand(
            scheduleId,
            resource.ClassroomId,
            resource.StartTime,
            resource.EndTime,
            resource.DayOfWeek
            );
    }
    
}