using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class UpdateClassroomCommandFromResourceAssembler
{
    public static UpdateClassroomCommand ToCommandFromResource(int id, UpdateClassroomResource resource)
    {
        return new UpdateClassroomCommand(id, resource.Code, resource.Capacity, resource.Campus);
    }
}