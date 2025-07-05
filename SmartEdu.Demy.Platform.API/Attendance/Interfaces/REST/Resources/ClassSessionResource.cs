namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record ClassSessionResource(int Id, long CourseId, DateTime Date, List<AttendanceRecordResource> Attendance);
