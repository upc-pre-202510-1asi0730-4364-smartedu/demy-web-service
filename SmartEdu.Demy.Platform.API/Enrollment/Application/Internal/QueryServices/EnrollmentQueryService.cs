using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

/// <summary>
/// Service that handles queries to retrieve enrollment data.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class EnrollmentQueryService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
    : IEnrollmentQueryService
{
    /// <summary>
    /// Handles retrieving all enrollments.
    /// </summary>
    /// <param name="query">Query object (unused in this implementation)</param>
    /// <returns>A list of all enrollments</returns>
    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsQuery query)
    {
        return await enrollmentRepository.ListAsync();
    }

    /// <summary>
    /// Handles retrieving a specific enrollment by its ID.
    /// </summary>
    /// <param name="query">Query containing the enrollment ID</param>
    /// <returns>The matching enrollment, or null if not found</returns>
    public async Task<Domain.Model.Aggregates.Enrollment?> Handle(GetEnrollmentByIdQuery query)
    {
        return await enrollmentRepository.FindByIdAsync(query.enrollmentId);
    }

    /// <summary>
    /// Handles retrieving all enrollments for a given student by student ID.
    /// </summary>
    /// <param name="query">Query containing the student ID</param>
    /// <returns>A list of enrollments for the specified student</returns>
    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentIdQuery query)
    {
        return await enrollmentRepository.FindAllByStudentIdAsync(query.studentId);
    }

    /// <summary>
    /// Handles retrieving all enrollments for a student by their DNI.
    /// </summary>
    /// <param name="query">Query containing the student's DNI</param>
    /// <returns>A list of enrollments for the student, or empty if the student is not found</returns>
    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentDniQuery query)
    {
        var student = await studentRepository.FindByDniAsync(query.studentDni);
        if (student == null) return Enumerable.Empty<Domain.Model.Aggregates.Enrollment>();

        return await enrollmentRepository.FindAllByStudentIdAsync(student.Id);
    }
}
