using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;


namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;


/// <summary>
/// Aggregate root representing a system user account with authentication and role-based authorization.
/// </summary>
public partial class UserAccount
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    [MaxLength(60)]
    public string FullName { get; set; }
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [MaxLength(100)]
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the hashed password of the user. This property is ignored in JSON serialization.
    /// </summary>
    [MaxLength(150)]
    [JsonIgnore] public string PasswordHash { get; set; }
    /// <summary>
    /// Gets the role assigned to the user.
    /// </summary>
    public Role Role { get;private set; }
    /// <summary>
    /// Gets the current account status.
    /// </summary>
    public AccountStatus Status { get; private set; }
    
    /// <summary>
    /// Initializes a new user account with default status as INACTIVE.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="fullName">The full name of the user.</param>
    /// <param name="email">The email address.</param>
    /// <param name="passwordHash">The hashed password.</param>
    /// <param name="role">The role assigned to the user.</param>
    public UserAccount(long userId, string fullName, string email, string passwordHash, Role role)
        : this(userId, fullName, email, passwordHash, role, AccountStatus.INACTIVE)
    {
    }
    
    /// <summary>
    /// Initializes a new user account with full parameters.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="fullName">The full name of the user.</param>
    /// <param name="email">The email address.</param>
    /// <param name="passwordHash">The hashed password.</param>
    /// <param name="role">The role assigned to the user.</param>
    /// <param name="status">The current account status.</param>
    public UserAccount(long userId, string fullName, string email, string passwordHash, Role role, AccountStatus  status)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Status = status;
    }
    /// <summary>
    /// Default constructor required by Entity Framework.
    /// </summary>
    protected UserAccount() { }
    
    /// <summary>
    /// Activates the user account by setting the status to ACTIVE.
    /// </summary>
    public void Activate() => Status = AccountStatus.ACTIVE;
    
    /// <summary>
    /// Deactivates the user account by setting the status to INACTIVE.
    /// </summary>
    public void Deactivate() => Status = AccountStatus.INACTIVE;
    /// <summary>
    /// Blocks the user account by setting the status to BLOCKED.
    /// </summary>
    public void Block() => Status = AccountStatus.BLOCKED;
    /// <summary>
    /// Updates the user account password hash.
    /// </summary>
    /// <param name="newHash">The new hashed password.</param>
    public void ChangePassword(string newHash) => PasswordHash = newHash;
    /// <summary>
    /// Updates the email address of the user.
    /// </summary>
    /// <param name="newEmail">The new email address.</param>
    public void UpdateEmail(string newEmail) => Email = newEmail;
    

    
}