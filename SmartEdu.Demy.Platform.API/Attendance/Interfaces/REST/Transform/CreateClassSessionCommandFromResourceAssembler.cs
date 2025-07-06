using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;
/// <summary>
/// Assembler class responsible for transforming REST resources into domain commands
/// related to class session creation.
/// </summary>
public static class CreateClassSessionCommandFromResourceAssembler 
{
    // <summary>
    /// Converts a <see cref="CreateClassSessionResource"/> resource into a <see cref="CreateClassSessionCommand"/>.
    /// </summary>
    /// <param name="resource">The incoming REST resource containing data to create a class session.</param>
    /// <returns>A domain command used to create a new class session.</returns>
    public static CreateClassSessionCommand ToCommandFromResource( CreateClassSessionResource resource) =>
    new CreateClassSessionCommand(resource.CourseId
        , resource.Date,
        resource.Attendance
            .Select(r => new AttendanceDraft(r.Dni, Enum.Parse<AttendanceStatus>(r.Status)))
        .ToList()
        );
}