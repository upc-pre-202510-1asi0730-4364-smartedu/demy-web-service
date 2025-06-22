using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

public static class AcademicPeriodResourceFromEntityAssembler
{
    public static AcademicPeriodResource ToResourceFromEntity(AcademicPeriod entity)
    {
        return new AcademicPeriodResource(entity.Id, entity.PeriodName, entity.PeriodDuration.StartDate,
            entity.PeriodDuration.EndDate, entity.IsActive);
    }
}