namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;

public class UserAccount
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    public AccountStatus Status { get; set; }

    protected UserAccount() { } 
    
    public UserAccount(long userId, string fullName, string email, string passwordHash, Role role)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Status = AccountStatus.INACTIVE;
    }

    public void Activate() => Status = AccountStatus.ACTIVE;
    public void Deactivate() => Status = AccountStatus.INACTIVE;
    public void Block() => Status = AccountStatus.BLOCKED;
    public void ChangePassword(string newHash) => PasswordHash = newHash;
    public void UpdateEmail(string newEmail) => Email = newEmail;
}