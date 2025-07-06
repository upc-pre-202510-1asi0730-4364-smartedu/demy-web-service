using System.Runtime.InteropServices.JavaScript;


using System.Collections.Generic;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;
namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;


/// <summary>
/// Represents a session for a specific course on a given date, including all associated attendance records.
/// </summary>
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
    /// <summary>
    /// Creates a new instance of <see cref="ClassSession"/> from a <see cref="CreateClassSessionCommand"/>.
    /// </summary>
    /// <param name="command">The command containing session details and attendance drafts.</param>
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