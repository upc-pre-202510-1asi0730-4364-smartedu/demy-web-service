using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class CreateCourseCommandFromResourceAssembler
{
    
    public static CreateCourseCommand ToCommandFromResource(CreateCourseResource resource)
    {
        return new CreateCourseCommand(resource.Name, resource.Code, resource.Description);
    }
}