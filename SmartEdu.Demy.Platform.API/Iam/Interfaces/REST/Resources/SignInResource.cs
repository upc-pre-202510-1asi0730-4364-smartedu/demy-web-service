namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

/// <summary>
/// Resource used for user sign-in.
/// </summary>
public class SignInResource
{
    /// <summary>
    /// The email address of the user attempting to sign in.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// The user's password in plain text.
    /// </summary>
    public string Password { get; set; }
}