using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;

public interface IWeeklyScheduleRepository : IBaseRepository<WeeklySchedule>
{
    Task<IEnumerable<WeeklySchedule>> FindByNameAsync(string name);
    
    Task<IEnumerable<WeeklySchedule>> FindByCourseIdAsync(int courseId);
    
    Task<IEnumerable<WeeklySchedule>> FindByClassroomIdAsync(int classroomId);
}