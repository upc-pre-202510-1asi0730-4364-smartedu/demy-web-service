using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class ClassroomResourceFromEntityAssembler
{
    public static ClassroomResource ToResourceFromEntity(Classroom entity)
    {
        return new ClassroomResource(entity.Id, entity.Code, entity.Capacity, entity.Campus);
    }
}