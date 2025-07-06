namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

/// <summary>
/// Resource used to create a new class session with attendance data.
/// </summary>
/// <param name="CourseId">The ID of the course for which the session is being created.</param>
/// <param name="Date">The date of the class session.</param>
/// <param name="Attendance">A list of attendance records to associate with the session.</param>
public record CreateClassSessionResource(long CourseId, DateOnly Date, List<AttendanceRecordResource> Attendance);