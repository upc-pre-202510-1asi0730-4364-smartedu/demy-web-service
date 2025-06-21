using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

public static class CreateAcademicPeriodCommandFromResourceAssembler
{
    public static CreateAcademicPeriodCommand ToCommandFromResource(CreateAcademicPeriodResource resource)
    {
        return new CreateAcademicPeriodCommand(
        resource.PeriodName, 
        resource.StartDate, 
        resource.EndDate, 
        resource.IsActive);
    }
}