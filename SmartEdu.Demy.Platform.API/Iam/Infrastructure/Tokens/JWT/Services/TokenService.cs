using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Tokens.JWT.Configuration;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Tokens.JWT.Services;

/// <summary>
///     Service for creating and validating JWT tokens.
/// </summary>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    /// <summary>
    ///     Generates a JWT token for the given user account.
    /// </summary>
    /// <param name="user">The user account to include in the token payload.</param>
    /// <returns>The generated JWT as a string.</returns>
    public string GenerateToken(UserAccount user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_tokenSettings.Secret);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiresInMinutes),
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(descriptor);
        return token;
    }

    /// <summary>
    ///     Validates a JWT token and extracts the user ID if valid.
    /// </summary>
    /// <param name="token">The JWT to validate.</param>
    /// <returns>The user ID if valid; otherwise, null.</returns>
    public async Task<long?> ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.UTF8.GetBytes(_tokenSettings.Secret);

        try
        {
            var validationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _tokenSettings.Audience,
                ClockSkew = TimeSpan.Zero
            });

            if (!validationResult.IsValid || validationResult.ClaimsIdentity is null)
                return null;

            var userIdClaim = validationResult.ClaimsIdentity.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var userId))
                return userId;

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}