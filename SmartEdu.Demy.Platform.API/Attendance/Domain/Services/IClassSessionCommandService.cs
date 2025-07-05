using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Services;

public interface IClassSessionCommandService
{
    
    Task<ClassSession> Handle(CreateClassSessionCommand command);
}