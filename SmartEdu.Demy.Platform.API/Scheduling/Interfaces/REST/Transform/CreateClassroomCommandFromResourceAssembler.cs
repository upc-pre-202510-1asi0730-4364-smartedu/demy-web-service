using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class CreateClassroomCommandFromResourceAssembler
{
    public static CreateClassroomCommand ToCommandFromResource(CreateClassroomResource resource)
    {
        return new CreateClassroomCommand(resource.Code, resource.Capacity, resource.Campus);
    }
}