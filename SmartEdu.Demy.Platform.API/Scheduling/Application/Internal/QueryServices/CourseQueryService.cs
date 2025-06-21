using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;

public class CourseQueryService(ICourseRepository courseRepository) : ICourseQueryService
{
    public async Task<IEnumerable<Course>> Handle(GetAllCoursesQuery query)
    {
        return await courseRepository.ListAsync();
    }
    
    public async Task<Course?> Handle(GetCourseByIdQuery query)
    {
        return await courseRepository.FindByIdAsync(query.CourseId);
    }
}