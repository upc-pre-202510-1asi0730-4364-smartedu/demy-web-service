using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

public class EnrollmentQueryService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
    : IEnrollmentQueryService
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsQuery query)
    {
        return await enrollmentRepository.ListAsync();
    }

    public async Task<Domain.Model.Aggregates.Enrollment?> Handle(GetEnrollmentByIdQuery query)
    {
        return await enrollmentRepository.FindByIdAsync(query.enrollmentId);
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentIdQuery query)
    {
        return await enrollmentRepository.FindAllByStudentIdAsync(query.studentId);
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentDniQuery query)
    {
        var student = await studentRepository.FindByDniAsync(query.studentDni);
        if (student == null) return Enumerable.Empty<Domain.Model.Aggregates.Enrollment>();

        return await enrollmentRepository.FindAllByStudentIdAsync(student.Id);
    }
}