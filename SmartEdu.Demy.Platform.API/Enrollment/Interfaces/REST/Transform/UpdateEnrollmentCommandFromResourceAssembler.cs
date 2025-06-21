using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class UpdateEnrollmentCommandFromResourceAssembler
    {
        public static UpdateEnrollmentCommand ToCommandFromResource(int enrollmentId, UpdateEnrollmentResource resource)
        {
            return new UpdateEnrollmentCommand(
                enrollmentId,
                resource.Amount,
                resource.Currency,
                resource.EnrollmentStatus,
                resource.PaymentStatus
            );
        }
    }
}
