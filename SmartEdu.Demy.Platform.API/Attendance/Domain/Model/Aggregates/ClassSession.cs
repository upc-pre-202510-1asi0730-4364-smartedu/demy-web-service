using System.Runtime.InteropServices.JavaScript;


using System.Collections.Generic;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;
namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
public partial class ClassSession
{
    public long Id { get; private set; }
    public long CourseId { get; private set; }
    public DateOnly Date { get; private set; }

    private readonly List<AttendanceRecord> _attendance = new();
    public IReadOnlyCollection<AttendanceRecord> Attendance => _attendance.AsReadOnly();
    

    public ClassSession(long courseId, IEnumerable<AttendanceRecord> attendanceRecords, DateOnly date)
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
        _attendance.AddRange(command.Attendance.Select(draft =>
            new AttendanceRecord(draft.Dni, draft.Status)
        ));

        Date = command.Date;
        
    }
    public ClassSession() { }

}