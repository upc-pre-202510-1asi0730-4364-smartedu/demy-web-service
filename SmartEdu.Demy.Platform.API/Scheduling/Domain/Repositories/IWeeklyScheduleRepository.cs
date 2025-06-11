using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;

public interface IWeeklyScheduleRepository : IBaseRepository<WeeklySchedule>
{
    
    Task<WeeklySchedule?> FindWeeklyScheduleByNameAsync(string name);
    
}