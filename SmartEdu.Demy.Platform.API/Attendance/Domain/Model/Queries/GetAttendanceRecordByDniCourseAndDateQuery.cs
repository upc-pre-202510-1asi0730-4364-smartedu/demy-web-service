namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;

public record GetAttendanceRecordByDniCourseAndDateQuery(long CourseId, string Dni, DateOnly StartDate, DateOnly EndDate);
