using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;

/// <summary>
/// Assembler class responsible for transforming <see cref="AttendanceRecord"/> domain entities
/// into <see cref="AttendanceReportResource"/> resources for external use.
/// </summary>
public static class AttendanceReportFromEntityAssembler
{
    /// <summary>
    /// Maps a domain <see cref="AttendanceRecord"/> and date to an <see cref="AttendanceReportResource"/>.
    /// </summary>
    /// <param name="entity">The attendance record entity to convert.</param>
    /// <param name="date">The date of the class session the record belongs to.</param>
    /// <returns>An <see cref="AttendanceReportResource"/> containing the mapped data.</returns>
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