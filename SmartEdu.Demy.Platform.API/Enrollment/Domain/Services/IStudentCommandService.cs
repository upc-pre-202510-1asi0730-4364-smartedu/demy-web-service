using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
/// <summary>
/// Student command service interface 
/// </summary>
public interface IStudentCommandService
{
    Task<Student?> Handle(CreateStudentCommand command);
    Task<bool> Handle(DeleteStudentCommand command);
    Task<Student?> Handle(UpdateStudentCommand command);
}