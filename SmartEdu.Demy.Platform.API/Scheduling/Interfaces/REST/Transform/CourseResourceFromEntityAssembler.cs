using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;

public static class CourseResourceFromEntityAssembler
{
    public static CourseResource ToResourceFromEntity(Course entity)
    {
        return new CourseResource(entity.Id, entity.Name, entity.Code, entity.Description);
    }
}