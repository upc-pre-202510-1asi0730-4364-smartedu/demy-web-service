using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface ICourseQueryService
{
    Task<IEnumerable<Course>> Handle(GetAllCoursesQuery query);
    
    Task<Course?> Handle(GetCourseByIdQuery query);
}