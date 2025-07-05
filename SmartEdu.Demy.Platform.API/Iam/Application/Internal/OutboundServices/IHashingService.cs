namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Defines operations for securely hashing and verifying passwords.
/// </summary>
public interface IHashingService
{
    /// <summary>
    ///     Generates a secure hash from a plain text password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>A hashed representation of the password.</returns>
    string HashPassword(string password);

    /// <summary>
    ///     Checks if a given password matches its hashed version.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="passwordHash">The stored hashed password.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    bool VerifyPassword(string password, string passwordHash);
}