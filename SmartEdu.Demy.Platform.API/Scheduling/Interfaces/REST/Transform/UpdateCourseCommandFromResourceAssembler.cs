using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class UpdateCourseCommandFromResourceAssembler
{
    public static UpdateCourseCommand ToCommandFromResource(int id, UpdateCourseResource resource)
    {
        return new UpdateCourseCommand(id, resource.Name, resource.Code, resource.Description);
    }
}