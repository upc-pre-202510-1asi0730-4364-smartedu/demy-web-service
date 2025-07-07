namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;
/// <summary>
/// Resource representation of an attendance record used in REST responses.
/// </summary>
/// <param name="Dni">The student's DNI (National ID).</param>
/// <param name="Status">The attendance status ("Present" or "Absent").</param>
public record AttendanceRecordResource(string Dni, string Status);
