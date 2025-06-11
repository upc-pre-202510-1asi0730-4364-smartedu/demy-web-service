using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IStudentQueryService
{
    Task<Student?> Handle(GetStudentByIdQuery query);
}