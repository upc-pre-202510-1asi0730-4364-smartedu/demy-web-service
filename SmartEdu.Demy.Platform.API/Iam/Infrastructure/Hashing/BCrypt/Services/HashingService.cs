using SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Hashing.BCrypt.Services;

/// <summary>
///     Service for hashing and verifying user passwords using the BCrypt algorithm.
/// </summary>
public class HashingService : IHashingService
{
    /// <summary>
    ///     Creates a secure hash from the given plain text password.
    /// </summary>
    /// <param name="password">The user's plain text password.</param>
    /// <returns>A hashed version of the password.</returns>
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    /// <summary>
    ///     Checks whether the provided password matches the stored hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="passwordHash">The stored hashed password to compare against.</param>
    /// <returns>True if the password is valid; otherwise, false.</returns>
    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}