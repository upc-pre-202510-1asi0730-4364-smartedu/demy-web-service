using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

public class StudentCommandService(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork)
    : IStudentCommandService
{
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
            // Log exception e
            return null;
        }
    }

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
