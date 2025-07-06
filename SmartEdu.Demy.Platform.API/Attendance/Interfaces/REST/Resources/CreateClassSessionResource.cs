namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record CreateClassSessionResource(long CourseId, DateOnly Date, List<AttendanceRecordResource> Attendance);