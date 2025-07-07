using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
/// <summary>
/// Represents an individual attendance record for a student identified by their DNI.
/// </summary>
public partial class AttendanceRecord
{

   
    public string Dni { get; private set; }
    
    public AttendanceStatus Status { get; private set; }

  
    

    
    /// <summary>
    /// Gets or sets the full name of the student.
    /// This property is not persisted in the database.
    /// </summary>
    [NotMapped]
    public string? StudentName { get; set; }

    protected AttendanceRecord() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="AttendanceRecord"/> class.
    /// </summary>
    /// <param name="dni">The student's DNI.</param>
    /// <param name="status">The initial attendance status.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="dni"/> is null.</exception>
    public AttendanceRecord(string dni, AttendanceStatus status)
    {
        Dni = dni ?? throw new ArgumentNullException(nameof(dni));
        Status = status;
    }


    public void UpdateStatus(AttendanceStatus newStatus)
    {
        Status = newStatus;
    }


}