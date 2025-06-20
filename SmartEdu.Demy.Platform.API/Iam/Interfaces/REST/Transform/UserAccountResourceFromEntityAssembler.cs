using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;

/// <summary>
/// Maps user account domain entity to REST resource
/// </summary>
public static class UserAccountResourceFromEntityAssembler
{
    public static UserAccountResource ToResource(UserAccount entity)
    {
        return new UserAccountResource(
            entity.UserId,
            entity.Email,
            entity.FullName,
            entity.Role.ToString()
        );
    }
}