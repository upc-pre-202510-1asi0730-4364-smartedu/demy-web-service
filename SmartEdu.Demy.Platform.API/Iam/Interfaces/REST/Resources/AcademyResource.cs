namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Represents an academy resource used in responses.
/// </summary>
public class AcademyResource
{
    /// <summary>
    /// Gets or sets the unique identifier of the academy.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the user ID associated with the academy.
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the academy.
    /// </summary>
    public string AcademyName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the RUC (unique tax identification number) of the academy.
    /// </summary>
    public string Ruc { get; set; } = string.Empty;
}