using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Attendance.Infrastructure.Repositories;

public class ClassSessionRepository(AppDbContext context) : BaseRepository<ClassSession>(context), IClassSessionRepository

{
    ///<inheritdoc />
    public async Task<ClassSession> FindByCourseAndDateAsync(long courseId, DateOnly date)
    {
      return await Context.Set<ClassSession>().FirstOrDefaultAsync(x => x.CourseId == courseId && x.Date == date);  
    }
    
    public async Task<List<AttendanceRecord>> FindAttendanceRecordsByDniCourseAndDateRangeAsync(
        long courseId, string dni, DateOnly startDate, DateOnly endDate)
    {
        return await Context.Set<ClassSession>()
            .Where(cs => cs.CourseId == courseId && cs.Date >= startDate && cs.Date <= endDate)
            .SelectMany(cs => cs.Attendance
                .Where(a => a.Dni == dni))
            .ToListAsync();
    }
    public async Task<List<ClassSession>> FindSessionsByCourseAndDateRangeAsync(
        long courseId, DateOnly startDate, DateOnly endDate)
    {
        return await Context.Set<ClassSession>()
            .Where(cs => cs.CourseId == courseId && cs.Date >= startDate && cs.Date <= endDate)
            .Include(cs => cs.Attendance) // asegúrate de incluir la relación
            .ToListAsync();
    }

}