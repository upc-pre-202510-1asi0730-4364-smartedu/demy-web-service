using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IAcademicPeriodQueryService
{
    Task<IEnumerable<AcademicPeriod>> Handle(GetAllAcademicPeriodsQuery query);
    Task<AcademicPeriod?> Handle(GetAcademicPeriodByIdQuery query);
}