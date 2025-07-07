using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;


/// <summary>
/// Contract for handling commands related to the creation and management of academies.
/// </summary>
public interface IAcademyCommandService
{
    /// <summary>
    /// Handles the creation of a new academy based on the given command.
    /// </summary>
    /// <param name="command">The command containing the necessary data to create the academy.</param>
    /// <returns>The newly created <see cref="Academy"/> entity.</returns>
    Task<Academy> Handle(CreateAcademyCommand command);

}