namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

public class AcademyResource
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string AcademyName { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
}