using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform
{
    public static class EnrollmentResourceFromEntityAssembler
    {
        public static EnrollmentResource ToResourceFromEntity(Domain.Model.Aggregates.Enrollment entity)
        {
            return new EnrollmentResource(
                entity.Id,
                entity.StudentId,
                entity.PeriodId,
                entity.Amount,
                entity.Currency,
                entity.EnrollmentStatus.ToString(),
                entity.PaymentStatus.ToString()
            );
        }
    }
}