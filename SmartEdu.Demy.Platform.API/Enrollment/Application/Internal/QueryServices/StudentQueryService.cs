using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

/// <summary>
///     Implementation of the IStudentQueryService interface
/// </summary>

public class StudentQueryService : IStudentQueryService
{
    private readonly IStudentRepository _studentRepository;

    public StudentQueryService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(GetStudentByIdQuery query)
    {
        return await _studentRepository.FindByIdAsync((int)query.StudentId);
    }
}
