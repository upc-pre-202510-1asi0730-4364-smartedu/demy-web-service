namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;



/// <summary>
/// Value object used to capture raw attendance input data during the creation of a class session.
/// This is used as a draft version before being converted into a proper <see cref="AttendanceRecord"/> entity.
/// </summary>
/// <param name="Dni">The DNI (national ID number) of the student.</param>
/// <param name="Status">The attendance status of the student (e.g., Present or Absent).</param>
public record AttendanceDraft(string Dni, AttendanceStatus Status);
