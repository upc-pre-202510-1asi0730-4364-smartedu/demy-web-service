using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using System.Collections.Generic; // para que compile el list

/// <summary>
/// Command for creating a new class session, including course ID, date, and attendance records.
/// </summary>
/// <param name="CourseId">The identifier of the course associated with the class session.</param>
/// <param name="Date">The date when the class session takes place.</param>
/// <param name="Attendance">The list of attendance drafts (DNI and status) for the session.</param>
public record CreateClassSessionCommand(
    long CourseId,
    DateOnly Date,
    List<AttendanceDraft> Attendance
);
