using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

public class Academy
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string AcademyName { get; set; }

    public string Ruc { get; set; }

    // Navigation property (si lo necesitas)
    public UserAccount User { get; set; }

    // Esto no se guarda en BD si usas EF
    public List<AcademicPeriod> Periods { get; set; } = new();

    public Academy(long userId, string academyName, string ruc)
    {
        UserId = userId;
        AcademyName = academyName;
        Ruc = ruc;
    }

    public void UpdateNameAndRuc(string newName, string newRuc)
    {
        AcademyName = newName;
        Ruc = newRuc;
    }
}
