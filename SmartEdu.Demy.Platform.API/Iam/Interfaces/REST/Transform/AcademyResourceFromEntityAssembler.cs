using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;


public static class AcademyResourceFromEntityAssembler
{
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