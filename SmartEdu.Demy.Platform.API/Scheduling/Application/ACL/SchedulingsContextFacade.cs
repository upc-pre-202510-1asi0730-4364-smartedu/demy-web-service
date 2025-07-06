using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.ACL;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.ACL;

public class SchedulingsContextFacade(IWeeklyScheduleQueryService weeklyScheduleQueryService): ISchedulingsContextFacade
{
    // inheritedDoc
    public async Task<int> FetchWeeklyScheduleIdByName(string name)
    {
        var getWeeklyScheduleByNameQuery = new GetWeeklyScheduleByNameQuery(name);
        var weeklySchedule = await weeklyScheduleQueryService.Handle(getWeeklyScheduleByNameQuery);
        return weeklySchedule?.Id ?? 0;
    }
}