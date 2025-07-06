using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Commands;
namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Services;

/// <summary>
/// Defines the contract for handling commands related to class sessions.
/// </summary>
public interface IClassSessionCommandService
{
    /// <summary>
    /// Handles the creation of a new class session based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the details of the class session to be created.</param>
    /// <returns>A task representing the asynchronous operation, with the newly created <see cref="ClassSession"/> as the result.</returns>
    Task<ClassSession> Handle(CreateClassSessionCommand command);
}