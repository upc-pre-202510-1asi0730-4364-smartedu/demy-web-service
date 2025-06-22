using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

public class AcademicPeriodQueryService(IAcademicPeriodRepository academicPeriodRepository)
    : IAcademicPeriodQueryService
{
    public async Task<IEnumerable<AcademicPeriod>> Handle(GetAllAcademicPeriodsQuery query)
    {
        return await academicPeriodRepository.ListAsync();
    }

    public async Task<AcademicPeriod?> Handle(GetAcademicPeriodByIdQuery query)
    {
        return await academicPeriodRepository.FindByIdAsync(query.Id);
    }
}