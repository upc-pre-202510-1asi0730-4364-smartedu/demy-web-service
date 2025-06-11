using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;

public static class CreateClassSessionCommandFromResourceAssembler 
{
    public static CreateClassSessionCommand ToCommandFromResource( CreateClassSessionResource resource) =>
    new CreateClassSessionCommand(resource.CourseId
        , resource.Date,
        resource.Attendance
            .Select(r=> new AttendanceRecord(r.StudentId,Enum.Parse<AttendanceStatus>(r.Status)))
        .ToList()
        );
}