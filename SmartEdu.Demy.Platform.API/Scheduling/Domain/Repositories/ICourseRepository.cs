using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;

public interface ICourseRepository : IBaseRepository<Course>
{
    Task<Course?> FindCourseByCodeAsync(string code);
}