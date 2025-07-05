using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using System.Collections.Generic; // para que compile el list


public record CreateClassSessionCommand(long  CourseId, DateTime Date, List<AttendanceRecord> Attendance);
