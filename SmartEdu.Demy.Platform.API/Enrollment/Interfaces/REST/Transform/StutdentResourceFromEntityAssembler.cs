using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

public static class StudentResourceFromEntityAssembler
{
    public static StudentResource ToResourceFromEntity(Student entity)
    {
        return new StudentResource(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Dni,
            entity.Sex.ToString(),
            entity.BirthDate,
            entity.Address,
            entity.PhoneNumber,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }
}