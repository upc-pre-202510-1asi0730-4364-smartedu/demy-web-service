using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Attendance.Infrastructure.Repositories;
/// <summary>
/// Repository implementation for managing <see cref="ClassSession"/> aggregates using Entity Framework Core.
/// </summary>
/// <param name="context">The application's database context.</param>
public class ClassSessionRepository(AppDbContext context) : BaseRepository<ClassSession>(context), IClassSessionRepository

{
    /// <inheritdoc />
    /// <summary>
    /// Finds a class session by its course ID and specific date.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="date">The specific date of the session.</param>
    /// <returns>The matching <see cref="ClassSession"/>, or null if not found.</returns>
    public async Task<ClassSession> FindByCourseAndDateAsync(int courseId, DateOnly date)
    {
      return await Context.Set<ClassSession>().FirstOrDefaultAsync(x => x.CourseId == courseId && x.Date == date);  
    }
    /// <summary>
    /// Retrieves attendance records for a student in a course within a specific date range.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="dni">The DNI of the student.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of <see cref="AttendanceRecord"/> objects.</returns>
    public async Task<List<AttendanceRecord>> FindAttendanceRecordsByDniCourseAndDateRangeAsync(
        int courseId, string dni, DateOnly startDate, DateOnly endDate)
    {
        return await Context.Set<ClassSession>()
            .Where(cs => cs.CourseId == courseId && cs.Date >= startDate && cs.Date <= endDate)
            .SelectMany(cs => cs.Attendance
                .Where(a => a.Dni == dni))
            .ToListAsync();
    }
    /// <summary>
    /// Retrieves all class sessions for a course within a specified date range, including attendance records.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of <see cref="ClassSession"/> entities.</returns>
    public async Task<List<ClassSession>> FindSessionsByCourseAndDateRangeAsync(
        int courseId, DateOnly startDate, DateOnly endDate)
    {
        return await Context.Set<ClassSession>()
            .Where(cs => cs.CourseId == courseId && cs.Date >= startDate && cs.Date <= endDate)
            .Include(cs => cs.Attendance) // asegúrate de incluir la relación
            .ToListAsync();
    }

}