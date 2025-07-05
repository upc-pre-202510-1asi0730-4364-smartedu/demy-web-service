using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

public class StudentQueryService(IStudentRepository studentRepository)
    : IStudentQueryService
{
    public async Task<Student?> Handle(GetStudentByIdQuery query)
    {
        return await studentRepository.FindByIdAsync(query.StudentId);
    }

    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery query)
    {
        return await studentRepository.ListAsync();
    }

    public async Task<Student?> Handle(GetStudentByDniQuery query)
    {
        return await studentRepository.FindByDniAsync(query.dni);
    }
}