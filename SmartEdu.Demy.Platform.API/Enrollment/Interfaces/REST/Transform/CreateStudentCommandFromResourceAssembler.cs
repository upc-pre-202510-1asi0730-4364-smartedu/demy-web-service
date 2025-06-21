using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

public static class CreateStudentCommandFromResourceAssembler
{
    public static CreateStudentCommand ToCommandFromResource(CreateStudentResource resource)
    {
        return new CreateStudentCommand(
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