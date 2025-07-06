using SmartEdu.Demy.Platform.API.Attendance.Application.Internal.OutboundServices.ACL;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;


namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.QueryServices;

public class ClassSessionQueryService(
    IClassSessionRepository classSessionRepository, 
    ExternalEnrollmentServiceForAttendance externalEnrollmentService
) : IClassSessionQueryService
{
    private readonly IClassSessionRepository _classSessionRepository = classSessionRepository;
    private readonly ExternalEnrollmentServiceForAttendance _externalEnrollmentService = externalEnrollmentService;

    public async Task<ClassSession> Handle(GetClassSessionByIdQuery query)
    {
        return await _classSessionRepository.FindByIdAsync(query.Id);
    }

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
    
    public async Task<List<ClassSession>> Handle(GetClassSessionsByCourseAndDateRangeQuery query)
    {
        var sessions = await _classSessionRepository.FindSessionsByCourseAndDateRangeAsync(
            query.CourseId, query.StartDate, query.EndDate
        );

        if (sessions == null || !sessions.Any()) return [];

        // Obtén el nombre del estudiante por su DNI
        var studentName = await _externalEnrollmentService.FetchStudentNameByDni(query.Dni);

        // Asigna el nombre a los registros de asistencia que coinciden
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
