using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

/// <summary>
/// Contract for querying user account information from the system.
/// </summary>
public interface IUserAccountQueryService
{
    /// <summary>
    /// Retrieves a user account by its unique ID.
    /// </summary>
    /// <param name="query">The query containing the user ID.</param>
    /// <returns>The user account if found; otherwise, null.</returns>
    Task<UserAccount?> Handle(GetUserAccountByIdQuery query);
    
    /// <summary>
    /// Retrieves all users with the ADMIN role.
    /// </summary>
    /// <returns>A list of admin user accounts.</returns>
    Task<IEnumerable<UserAccount>> FindAdminsAsync();
    
    /// <summary>
    /// Retrieves all users with the TEACHER role.
    /// </summary>
    /// <returns>A list of teacher user accounts.</returns>
    Task<IEnumerable<UserAccount>> FindTeachersAsync();
    
    /// <summary>
    /// Retrieves a user account by its email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>The matching user account, or null if not found.</returns>
    Task<UserAccount> GetByEmailAsync(string email);
    

}