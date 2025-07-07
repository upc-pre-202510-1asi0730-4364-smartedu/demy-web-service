namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Tokens.JWT.Configuration;

/// <summary>
///     Represents configuration options for JWT tokens.
/// </summary>
public class TokenSettings
{
    /// <summary>
    ///     The secret key used for signing tokens.
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    ///     The token issuer.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    ///     The token audience.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    ///     Token expiration in minutes.
    /// </summary>
    public int ExpiresInMinutes { get; set; }
}