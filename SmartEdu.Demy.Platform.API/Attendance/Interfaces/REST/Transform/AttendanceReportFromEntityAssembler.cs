using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;


public static class AttendanceReportFromEntityAssembler
{
    public static AttendanceReportResource ToResourceFromEntity(AttendanceRecord entity, DateOnly date)
    {
        return new AttendanceReportResource(
            entity.Dni,
            entity.StudentName ?? string.Empty,
            entity.Status.ToString(),
            date
        );
    }
}