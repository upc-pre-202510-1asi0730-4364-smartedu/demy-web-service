using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class UpdateAcademicPeriodCommandFromResourceAssembler
    {
        public static UpdateAcademicPeriodCommand ToCommandFromResource(int id, UpdateAcademicPeriodResource resource)
        {
            return new UpdateAcademicPeriodCommand(
                id,
                resource.PeriodName,
                resource.StartDate,
                resource.EndDate,
                resource.IsActive
            );
        }
    }
}