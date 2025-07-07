using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;

/// <summary>
/// Assembler class for creating <see cref="ClassSessionReportResource"/> objects from
/// <see cref="ClassSession"/> domain entities.
/// </summary>
public static class ClassSessionReportFromEntityAssembler
{
    /// <summary>
    /// Converts a list of <see cref="ClassSession"/> entities into a <see cref="ClassSessionReportResource"/>,
    /// filtering attendance records by the specified DNI.
    /// </summary>
    /// <param name="courseId">The ID of the course for which the report is generated.</param>
    /// <param name="dni">The DNI of the student to filter attendance records.</param>
    /// <param name="sessions">The list of class sessions to include in the report.</param>
    /// <returns>A <see cref="ClassSessionReportResource"/> containing the attendance data for the specified student.</returns>
    public static ClassSessionReportResource ToResourceFromEntities(int courseId, string dni, List<ClassSession> sessions)
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
