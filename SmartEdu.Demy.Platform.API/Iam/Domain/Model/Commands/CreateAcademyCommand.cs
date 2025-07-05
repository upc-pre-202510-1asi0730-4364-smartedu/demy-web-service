namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

public record CreateAcademyCommand(long UserId, string AcademyName, string Ruc);
