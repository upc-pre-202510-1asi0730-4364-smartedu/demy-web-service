using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;

public static class ClassSessionReportFromEntityAssembler
{
    public static ClassSessionReportResource ToResourceFromEntities(long courseId, string dni, List<ClassSession> sessions)
    {
        var attendance = sessions
            .SelectMany(session =>
                session.Attendance
                    .Where(record => record.Dni == dni)
                    .Select(record =>
                        AttendanceReportFromEntityAssembler.ToResourceFromEntity(record, session.Date)
                    )
            )
            .ToList();

        return new ClassSessionReportResource(courseId, attendance);
    }
}
