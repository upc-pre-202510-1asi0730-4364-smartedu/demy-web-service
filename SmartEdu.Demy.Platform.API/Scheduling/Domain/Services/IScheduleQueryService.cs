using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface IScheduleQueryService
{
    Task<IEnumerable<Schedule>> Handle(GetSchedulesByTeacherIdQuery query);
}