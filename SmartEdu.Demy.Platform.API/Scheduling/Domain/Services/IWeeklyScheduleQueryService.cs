using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface IWeeklyScheduleQueryService
{
    Task<IEnumerable<WeeklySchedule>> Handle(GetAllWeeklySchedulesQuery query);
    
    Task<WeeklySchedule?> Handle(GetWeeklyByIdQuery query);
}