using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IStudentQueryService
{
    Task<Student?> Handle(GetStudentByIdQuery query);
    Task<IEnumerable<Student>> Handle(GetAllStudentsQuery query);
    Task<Student?> Handle(GetStudentByDniQuery query);
}