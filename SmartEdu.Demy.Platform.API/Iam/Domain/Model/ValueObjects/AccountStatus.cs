namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;

/// <summary>
/// Represents the current status of a user account.
/// </summary>
public enum AccountStatus
{
    /// <summary>
    /// The account is active and can access the platform.
    /// </summary>
    ACTIVE,
    
    /// <summary>
    /// The account is inactive and cannot be used until reactivated.
    /// </summary>
    INACTIVE,
    
    /// <summary>
    /// The account is blocked due to policy violations or security issues.
    /// </summary>
    BLOCKED
}