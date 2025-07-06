using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;

public partial class AttendanceRecord
{

   
    public string Dni { get; private set; }
    
    public AttendanceStatus Status { get; private set; }

    // Navigation property
    

    // Transient / NotMapped property
    [NotMapped]
    public string? StudentName { get; set; }

    protected AttendanceRecord() { }

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