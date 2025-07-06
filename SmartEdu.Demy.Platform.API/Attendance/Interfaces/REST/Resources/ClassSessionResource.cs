namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record ClassSessionResource(long Id, long CourseId, DateOnly Date, List<AttendanceRecordResource> Attendance);
