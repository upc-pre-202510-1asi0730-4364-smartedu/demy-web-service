using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;

public class CourseRepository(AppDbContext context) 
    : BaseRepository<Course>(context), ICourseRepository
{
    
    public async Task<Course?> FindCourseByCodeAsync(string code)
    {
        return Context.Set<Course>().FirstOrDefault(c => c.Code == code);
    }
}