using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;

/// <summary>
/// Provides methods to transform Academy domain entities into REST resources.
/// </summary>
public static class AcademyResourceFromEntityAssembler
{
    /// <summary>
    /// Converts an <see cref="Academy"/> domain entity into an <see cref="AcademyResource"/> REST resource.
    /// </summary>
    /// <param name="entity">The academy domain entity.</param>
    /// <returns>An <see cref="AcademyResource"/> representing the academy.</returns>
    public static AcademyResource ToResource(Academy entity)
    {
        return new AcademyResource
        {
            Id = entity.Id,
            UserId = entity.UserId,
            AcademyName = entity.AcademyName,
            Ruc = entity.Ruc
        };
    }
}