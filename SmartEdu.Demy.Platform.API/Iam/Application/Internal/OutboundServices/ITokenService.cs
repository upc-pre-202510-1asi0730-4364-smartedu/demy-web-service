using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Provides functionality for creating and validating JWT tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    ///     Issues a JWT for the specified user account.
    /// </summary>
    /// <param name="user">The user account to include in the token.</param>
    /// <returns>The generated JWT as a string.</returns>
    string GenerateToken(UserAccount user);

    /// <summary>
    ///     Validates a JWT and extracts the user identifier if valid.
    /// </summary>
    /// <param name="token">The JWT to validate.</param>
    /// <returns>The user ID if the token is valid; otherwise, null.</returns>
    Task<long?> ValidateToken(string token);
}