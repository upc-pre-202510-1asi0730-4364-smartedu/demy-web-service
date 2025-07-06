namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

// <summary>
/// Resource used to represent a single entry in the attendance report.
/// </summary>
/// <param name="Dni">The student's DNI (National ID).</param>
/// <param name="StudentName">The full name of the student.</param>
/// <param name="Status">The attendance status ("Present" or "Absent").</param>
/// <param name="Date">The date of the attendance record.</param>
public record AttendanceReportResource(string Dni,string StudentName, string Status, DateOnly Date);
