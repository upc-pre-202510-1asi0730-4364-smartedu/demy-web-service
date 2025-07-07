namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;


/// <summary>
/// Resource representing a class session report, including all attendance records for a specific course.
/// </summary>
/// <param name="CourseId">The ID of the course associated with the class session.</param>
/// <param name="Attendance">The list of attendance report entries for this class session.</param>
public record ClassSessionReportResource(int CourseId, List<AttendanceReportResource> Attendance);
