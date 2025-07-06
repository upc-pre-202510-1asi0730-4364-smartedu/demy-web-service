using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

/// <summary>
/// Service that handles commands to create, update, and delete students.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class StudentCommandService(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork)
    : IStudentCommandService
{
    /// <summary>
    /// Handles creating a new student.
    /// </summary>
    /// <param name="command">Command with student data</param>
    /// <returns>The created Student, or null if an error occurs</returns>
    public async Task<Student?> Handle(CreateStudentCommand command)
    {
        var student = new Student(command);
        try
        {
            await studentRepository.AddAsync(student);
            await unitOfWork.CompleteAsync();
            return student;
        }
        catch (Exception e)
        {
            throw new Exception("Error creating student", e);
        }
    }

    /// <summary>
    /// Handles updating an existing student.
    /// </summary>
    /// <param name="command">Command with updated student data</param>
    /// <returns>The updated Student, or null if not found or on error</returns>
    public async Task<Student?> Handle(UpdateStudentCommand command)
    {
        var student = await studentRepository.FindByIdAsync(command.StudentId);
        if (student == null) return null;

        try
        {
            student.UpdateInformation(
                command.FirstName,
                command.LastName,
                command.Dni,
                Enum.Parse<ESex>(command.Sex, ignoreCase: true),
                command.BirthDate,
                command.Address,
                command.PhoneNumber
            );
            studentRepository.Update(student);
            await unitOfWork.CompleteAsync();
            return student;
        }
        catch (Exception e)
        {
            // Log exception e
            return null;
        }
    }

    /// <summary>
    /// Handles deleting a student.
    /// </summary>
    /// <param name="command">Command specifying the student to delete</param>
    /// <returns>True if deleted successfully; false otherwise</returns>
    public async Task<bool> Handle(DeleteStudentCommand command)
    {
        var student = await studentRepository.FindByIdAsync(command.StudentId);
        if (student == null) return false;

        try
        {
            studentRepository.Remove(student);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            // Log exception e
            return false;
        }
    }
}
