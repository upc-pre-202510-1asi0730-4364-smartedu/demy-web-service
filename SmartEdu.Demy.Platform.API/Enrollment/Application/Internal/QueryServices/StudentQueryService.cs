using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.QueryServices;

/// <summary>
/// Service that handles queries to retrieve student data.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class StudentQueryService(IStudentRepository studentRepository)
    : IStudentQueryService
{
    /// <summary>
    /// Handles retrieving a student by their ID.
    /// </summary>
    /// <param name="query">Query containing the student ID</param>
    /// <returns>The matching student, or null if not found</returns>
    public async Task<Student?> Handle(GetStudentByIdQuery query)
    {
        return await studentRepository.FindByIdAsync(query.StudentId);
    }

    /// <summary>
    /// Handles retrieving all students.
    /// </summary>
    /// <param name="query">Query object (not used directly)</param>
    /// <returns>A list of all students</returns>
    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery query)
    {
        return await studentRepository.ListAsync();
    }

    /// <summary>
    /// Handles retrieving a student by their DNI.
    /// </summary>
    /// <param name="query">Query containing the student's DNI</param>
    /// <returns>The matching student, or null if not found</returns>
    public async Task<Student?> Handle(GetStudentByDniQuery query)
    {
        return await studentRepository.FindByDniAsync(query.dni);
    }
}