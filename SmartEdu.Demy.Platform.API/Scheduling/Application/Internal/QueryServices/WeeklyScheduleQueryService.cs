using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;

/// <summary>
/// Weekly schedule query service implementation
/// </summary>
public class WeeklyScheduleQueryService(IWeeklyScheduleRepository weeklyScheduleRepository) 
    : IWeeklyScheduleQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<WeeklySchedule>> Handle(GetAllWeeklySchedulesQuery query)
    {
        return await weeklyScheduleRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<WeeklySchedule?> Handle(GetWeeklyByIdQuery query)
    {
        return await weeklyScheduleRepository.FindByIdAsync(query.WeeklyScheduleId);
    }
    
    public async Task<WeeklySchedule?> Handle(GetWeeklyScheduleByNameQuery query)
    {
        return await weeklyScheduleRepository.FindWeeklyScheduleByNameAsync(query.Name);
    }
}