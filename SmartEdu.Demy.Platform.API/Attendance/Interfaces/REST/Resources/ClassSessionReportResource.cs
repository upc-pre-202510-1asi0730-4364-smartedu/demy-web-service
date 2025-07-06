namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record ClassSessionReportResource(long CourseId, List<AttendanceReportResource> Attendance);
