using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
/// <summary>
/// Defines the contract for managing and querying class session aggregates in the persistence layer.
/// </summary>
public interface IClassSessionRepository : IBaseRepository<ClassSession>
{
    /// <summary>
    /// Retrieves a class session by course ID and specific date, if it exists.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="date">The date of the class session.</param>
    /// <returns>A task that returns the matching <see cref="ClassSession"/>, or null if not found.</returns>
    Task<ClassSession> FindByCourseAndDateAsync(long courseId, DateOnly date); 
    
    /// <summary>
    /// Retrieves attendance records by course ID, student DNI, and a date range.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="dni">The DNI of the student.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A task that returns a list of matching <see cref="AttendanceRecord"/> instances.</returns>
    Task<List<AttendanceRecord>> FindAttendanceRecordsByDniCourseAndDateRangeAsync(long courseId, string dni, DateOnly startDate, DateOnly endDate);
    /// <summary>
    /// Retrieves all class sessions for a course within a specified date range.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A task that returns a list of matching <see cref="ClassSession"/> instances.</returns>
    Task<List<ClassSession>> FindSessionsByCourseAndDateRangeAsync(long courseId, DateOnly startDate, DateOnly endDate);

}