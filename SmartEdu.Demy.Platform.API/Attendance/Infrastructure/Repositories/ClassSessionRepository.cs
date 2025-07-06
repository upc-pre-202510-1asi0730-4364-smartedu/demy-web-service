using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Attendance.Infrastructure.Repositories;

public class ClassSessionRepository(AppDbContext context) : BaseRepository<ClassSession>(context), IClassSessionRepository

{
    ///<inheritdoc />
    public async Task<ClassSession> FindByCourseAndDateAsync(long courseId, DateOnly date)
    {
      return await Context.Set<ClassSession>().FirstOrDefaultAsync(x => x.CourseId == courseId && x.Date == date);  
    }
}