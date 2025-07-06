using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;


namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;


public partial class UserAccount
{
    public long UserId { get; set; }
    
    [MaxLength(60)]
    public string FullName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    
    [MaxLength(150)]
    [JsonIgnore] public string PasswordHash { get; set; }
    public Role Role { get;private set; }
    public AccountStatus Status { get; private set; }
    
    public UserAccount(long userId, string fullName, string email, string passwordHash, Role role)
        : this(userId, fullName, email, passwordHash, role, AccountStatus.INACTIVE)
    {
    }
    public UserAccount(long userId, string fullName, string email, string passwordHash, Role role, AccountStatus  status)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Status = status;
    }

    protected UserAccount() { }

    public void Activate() => Status = AccountStatus.ACTIVE;
    public void Deactivate() => Status = AccountStatus.INACTIVE;
    public void Block() => Status = AccountStatus.BLOCKED;
    public void ChangePassword(string newHash) => PasswordHash = newHash;
    public void UpdateEmail(string newEmail) => Email = newEmail;
    

    
}