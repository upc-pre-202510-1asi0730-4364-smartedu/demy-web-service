namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;

public record GetAttendanceRecordByDniCourseAndDateQuery(int CourseId, string Dni, DateOnly StartDate, DateOnly EndDate);
