using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class StudentResourceFromEntityAssembler
    {
        public static StudentResource ToResourceFromEntity(Student entity)
        {
            return new StudentResource(
                entity.Id,
                entity.Name.FirstName,
                entity.Name.LastName,
                entity.Dni.Value,
                entity.Sex.ToString(),
                entity.BirthDate,
                entity.Address,
                entity.PhoneNumber.Value
            );
        }
    }
}