using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class UpdateStudentCommandFromResourceAssembler
    {
        public static UpdateStudentCommand ToCommandFromResource(int studentId, UpdateStudentResource resource)
        {
            return new UpdateStudentCommand(
                studentId,
                resource.FirstName,
                resource.LastName,
                resource.Dni,
                resource.Sex,
                resource.BirthDate,
                resource.Address,
                resource.PhoneNumber
            );
        }
    }
}
