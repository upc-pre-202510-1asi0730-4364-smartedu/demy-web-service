using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class CreateEnrollmentCommandFromResourceAssembler
    {
        public static CreateEnrollmentCommand ToCommandFromResource(CreateEnrollmentResource resource)
        {
            return new CreateEnrollmentCommand(
                resource.StudentId,
                resource.AcademicPeriodId,
                resource.Amount,
                resource.Currency,
                resource.EnrollmentStatus,
                resource.PaymentStatus
            );
        }
    }
}