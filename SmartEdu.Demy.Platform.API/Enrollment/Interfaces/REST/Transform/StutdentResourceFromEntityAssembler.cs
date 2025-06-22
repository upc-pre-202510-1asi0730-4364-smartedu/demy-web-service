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
                // Asumiendo que PersonName tiene una propiedad FullName:
                entity.Name.FullName,
                // Extraemos el valor interno de Dni
                entity.Dni.Value,
                // Convertimos el enum a string
                entity.Sex.ToString(),
                entity.BirthDate,
                entity.Address,
                // Extraemos el valor interno de PhoneNumber
                entity.PhoneNumber.Value
            );
        }
    }
}