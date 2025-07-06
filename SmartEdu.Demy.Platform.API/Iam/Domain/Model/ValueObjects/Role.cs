namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;


/// <summary>
/// Defines the access level or responsibilities assigned to a user account within the system.
/// </summary>
public enum Role
{
    /// <summary>
    /// Administrator role. Has full permissions across the entire platform, including user and academy management.
    /// </summary>
    ADMIN,
    /// <summary>
    /// Teacher role. Has limited access focused on educational and academic tasks.
    /// </summary>
    TEACHER
}