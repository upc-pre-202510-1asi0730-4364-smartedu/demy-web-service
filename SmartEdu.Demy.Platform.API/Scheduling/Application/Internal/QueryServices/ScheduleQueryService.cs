using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;

/// <summary>
/// Schedule query service implementation
/// </summary>
public class ScheduleQueryService(IScheduleRepository scheduleRepository) : IScheduleQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Schedule>> Handle(GetSchedulesByTeacherIdQuery query)
    {
        return await scheduleRepository.FindByTeacherIdAsync(query.TeacherId);
    }
}