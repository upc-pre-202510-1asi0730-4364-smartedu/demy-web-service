namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Public representation of a user account (admin or teacher)
/// </summary>
public record UserAccountResource(
    long Id,
    string Email,
    string FullName,
    string Role
);