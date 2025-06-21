namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record CreateClassSessionResource(long CourseId, DateTime Date, List<AttendanceRecordResource> Attendance);