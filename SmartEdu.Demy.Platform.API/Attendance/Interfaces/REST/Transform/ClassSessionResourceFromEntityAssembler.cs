using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;


/// <summary>
/// Assembler class for transforming <see cref="ClassSession"/> domain entities
/// into <see cref="ClassSessionResource"/> REST resources.
/// </summary>
public class ClassSessionResourceFromEntityAssembler
{
    
    /// <summary>
    /// Converts a <see cref="ClassSession"/> entity into a <see cref="ClassSessionResource"/> resource.
    /// </summary>
    /// <param name="entity">The <see cref="ClassSession"/> domain entity.</param>
    /// <returns>A <see cref="ClassSessionResource"/> representation of the domain entity.</returns>
    public static ClassSessionResource ToResourceFromEntity(ClassSession entity) =>
    new ClassSessionResource(entity.Id, 
        entity.CourseId, entity.Date,
        entity.Attendance
            .Select(a => new AttendanceRecordResource(a.Dni, a.Status.ToString()))
            .ToList()
        );
}