namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new academy.
/// </summary>
/// <param name="UserId">The ID of the user who owns the academy.</param>
/// <param name="AcademyName">The name of the academy.</param>
/// <param name="Ruc">The RUC (unique tax identification number) of the academy.</param>
public record CreateAcademyResource(
    long UserId,
    string AcademyName,
    string Ruc
);