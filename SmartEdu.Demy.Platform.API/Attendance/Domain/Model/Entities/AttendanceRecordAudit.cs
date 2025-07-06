namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
public partial class AttendanceRecord:IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")]public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")]public DateTimeOffset? UpdatedDate { get; set; }
}
