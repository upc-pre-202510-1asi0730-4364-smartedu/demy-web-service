namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a class session with attendance details.
/// </summary>
/// <param name="Id">The unique identifier of the class session.</param>
/// <param name="CourseId">The ID of the course associated with this session.</param>
/// <param name="Date">The date on which the class session occurred.</param>
/// <param name="Attendance">A list of attendance records for students in this session.</param>
public record ClassSessionResource(int Id, int CourseId, DateOnly Date, List<AttendanceRecordResource> Attendance);
