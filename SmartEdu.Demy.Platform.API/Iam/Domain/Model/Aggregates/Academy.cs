using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;


/// <summary>
/// Aggregate root representing an educational academy entity, associated with a user account.
/// </summary>
public class Academy
{
    /// <summary>
    /// Gets or sets the unique identifier of the academy.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the user who owns the academy.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the academy.
    /// </summary>
    public string AcademyName { get; set; }

    /// <summary>
    /// Gets or sets the RUC (Registro Único de Contribuyente) of the academy.
    /// </summary>
    public string Ruc { get; set; }

    /// <summary>
    /// Navigation property to the associated <see cref="UserAccount"/>.
    /// </summary>
    // Navigation property (si lo necesitas)
    public UserAccount User { get; set; }

    /// <summary>
    /// Gets or sets the academic periods associated with the academy.
    /// Not persisted in the database.
    /// </summary>
    // Esto no se guarda en BD si usas EF
    public List<AcademicPeriod> Periods { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Academy"/> class.
    /// </summary>
    /// <param name="userId">The user ID associated with the academy.</param>
    /// <param name="academyName">The name of the academy.</param>
    /// <param name="ruc">The RUC (tax ID) of the academy.</param>
    public Academy(long userId, string academyName, string ruc)
    {
        UserId = userId;
        AcademyName = academyName;
        Ruc = ruc;
    }

    /// <summary>
    /// Updates the academy's name and RUC.
    /// </summary>
    /// <param name="newName">The new name for the academy.</param>
    /// <param name="newRuc">The new RUC for the academy.</param>
    public void UpdateNameAndRuc(string newName, string newRuc)
    {
        AcademyName = newName;
        Ruc = newRuc;
    }
}
