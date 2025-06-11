using System.Runtime.InteropServices.JavaScript;


using System.Collections.Generic;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;
namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
public partial class ClassSession
{
    public int Id { get; private set; }
    public long CourseId { get; private set; }
    public DateTime Date { get; private set; }

    private readonly List<AttendanceRecord> _attendance = new();
    public IReadOnlyCollection<AttendanceRecord> Attendance => _attendance.AsReadOnly();
    

    public ClassSession(long courseId, IEnumerable<AttendanceRecord> attendanceRecords, DateTime date)
    {
        CourseId = courseId;
        _attendance.AddRange(attendanceRecords);
        Date = date;

    }
    // <summary>
    // command 
    public ClassSession(CreateClassSessionCommand command)
    {
        CourseId = command.CourseId; // for example
        _attendance.AddRange(command.Attendance); 
        Date = command.Date;
        
    }
    public ClassSession() { }

}