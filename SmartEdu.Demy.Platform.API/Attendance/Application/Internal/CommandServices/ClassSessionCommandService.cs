using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;


namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.CommandServices;

/// <summary>
/// Service responsible for handling class session creation commands.
/// </summary>
public class ClassSessionCommandService(IClassSessionRepository classSessionRepository, IUnitOfWork unitOfWork)
: IClassSessionCommandService
{
    /// <summary>
    /// Handles the creation of a new class session, validating uniqueness by course and date.
    /// </summary>
    /// <param name="command">Command containing class session details.</param>
    /// <returns>The newly created <see cref="ClassSession"/> instance.</returns>
    /// <exception cref="Exception">Thrown if a session already exists for the specified course and date.</exception>
    public async Task<ClassSession> Handle(CreateClassSessionCommand command)
    {
        var classSession = await classSessionRepository.FindByCourseAndDateAsync(command.CourseId, command.Date);
        if(classSession != null)
            throw new Exception("Class session with this courseId and date already exists");
        classSession = new ClassSession(command);
        try
        {
         await classSessionRepository.AddAsync(classSession);
         await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return classSession;
    }
    
}