using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;

public record AttendanceRecord(long StudentId,AttendanceStatus Status);