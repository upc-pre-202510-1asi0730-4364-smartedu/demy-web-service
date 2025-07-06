namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

public record AttendanceReportResource(string Dni,string StudentName, string Status, DateOnly Date);
