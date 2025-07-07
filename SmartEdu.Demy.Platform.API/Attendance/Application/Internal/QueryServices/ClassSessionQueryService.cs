using SmartEdu.Demy.Platform.API.Attendance.Application.Internal.OutboundServices.ACL;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;


namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.QueryServices;

/// <summary>
/// Service that handles queries related to class sessions and attendance records.
/// </summary>
public class ClassSessionQueryService(
    IClassSessionRepository classSessionRepository, 
    ExternalEnrollmentServiceForAttendance externalEnrollmentService
) : IClassSessionQueryService
{
    private readonly IClassSessionRepository _classSessionRepository = classSessionRepository;
    private readonly ExternalEnrollmentServiceForAttendance _externalEnrollmentService = externalEnrollmentService;
    /// <summary>
    /// Retrieves a class session by its unique ID.
    /// </summary>
    /// <param name="query">Query containing the class session ID.</param>
    /// <returns>The matching <see cref="ClassSession"/> or null if not found.</returns>
    public async Task<ClassSession> Handle(GetClassSessionByIdQuery query)
    {
        return await _classSessionRepository.FindByIdAsync(query.Id);
    }

    /// <summary>
    /// Retrieves attendance records by DNI, course ID, and date range.
    /// Enriches each record with the student's name from the external service.
    /// </summary>
    /// <param name="query">Query parameters including DNI, course ID, and date range.</param>
    /// <returns>List of <see cref="AttendanceRecord"/> for the student in the given range.</returns>
    public async Task<List<AttendanceRecord>> Handle(GetAttendanceRecordByDniCourseAndDateQuery query)
    {
        var records = await _classSessionRepository
            .FindAttendanceRecordsByDniCourseAndDateRangeAsync(query.CourseId, query.Dni, query.StartDate, query.EndDate);

        if (records == null || !records.Any()) return [];

        var studentName = await _externalEnrollmentService.FetchStudentNameByDni(query.Dni);

        foreach (var record in records)
        {
            record.StudentName = studentName;
        }

        return records;
    }
    /// <summary>
    /// Retrieves all class sessions for a course within a specific date range and enriches matching attendance records with the student's name.
    /// </summary>
    /// <param name="query">Query including course ID, date range, and DNI.</param>
    /// <returns>List of <see cref="ClassSession"/> with attendance records for the specified student.</returns>
    public async Task<List<ClassSession>> Handle(GetClassSessionsByCourseAndDateRangeQuery query)
    {
        var sessions = await _classSessionRepository.FindSessionsByCourseAndDateRangeAsync(
            query.CourseId, query.StartDate, query.EndDate
        );

        if (sessions == null || !sessions.Any()) return [];

   
        var studentName = await _externalEnrollmentService.FetchStudentNameByDni(query.Dni);

        
        foreach (var session in sessions)
        {
            foreach (var record in session.Attendance)
            {
                if (record.Dni == query.Dni)
                {
                    record.StudentName = studentName;
                }
            }
        }

        return sessions;
    }

}
