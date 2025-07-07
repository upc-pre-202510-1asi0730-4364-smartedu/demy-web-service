namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
/// Command to request the creation of a new academy.
/// </summary>
/// <param name="UserId">The ID of the user who owns the academy.</param>
/// <param name="AcademyName">The name of the academy to be created.</param>
/// <param name="Ruc">The RUC (Registro Único de Contribuyente) of the academy.</param>
public record CreateAcademyCommand(long UserId, string AcademyName, string Ruc);
