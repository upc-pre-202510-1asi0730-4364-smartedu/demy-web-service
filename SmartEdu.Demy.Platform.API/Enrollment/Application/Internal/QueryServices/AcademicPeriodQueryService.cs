using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

/// <summary>
/// Service that handles queries to retrieve academic period data.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class AcademicPeriodQueryService(IAcademicPeriodRepository academicPeriodRepository)
    : IAcademicPeriodQueryService
{
    /// <summary>
    /// Handles retrieving all academic periods.
    /// </summary>
    /// <param name="query">Query object </param>
    /// <returns>A list of all academic periods</returns>
    public async Task<IEnumerable<AcademicPeriod>> Handle(GetAllAcademicPeriodsQuery query)
    {
        return await academicPeriodRepository.ListAsync();
    }

    /// <summary>
    /// Handles retrieving a specific academic period by its ID.
    /// </summary>
    /// <param name="query">Query containing the academic period ID</param>
    /// <returns>The matching academic period, or null if not found</returns>
    public async Task<AcademicPeriod?> Handle(GetAcademicPeriodByIdQuery query)
    {
        return await academicPeriodRepository.FindByIdAsync(query.Id);
    }
}