using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;

public class ClassSessionResourceFromEntityAssembler
{
    public static ClassSessionResource ToResourceFromEntity(ClassSession entity) =>
    new ClassSessionResource(entity.Id, 
        entity.CourseId, entity.Date,
        entity.Attendance
            .Select(a => new AttendanceRecordResource(a.Dni, a.Status.ToString()))
            .ToList()
        );
}