namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

public record CreateAcademyResource(
    long UserId,
    string AcademyName,
    string Ruc
);