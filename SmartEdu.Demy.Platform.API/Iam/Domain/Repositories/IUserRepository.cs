using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;


/**
 * <summary>
 *     Repository interface for managing UserAccount entities
 * </summary>
 */
public interface IUserAccountRepository : IBaseRepository<UserAccount>
{
    /// <summary>
    /// Find a user account by username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <returns>The UserAccount if found; otherwise, null</returns>
    UserAccount? FindByUsername(string username);

    /// <summary>
    /// Check if a user exists with the given username
    /// </summary>
    /// <param name="username">The username to check</param>
    /// <returns>True if exists, otherwise false</returns>
    bool ExistsByUsername(string username);

    /// <summary>
    /// Find a user account by ID
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The UserAccount if found; otherwise, null</returns>
    UserAccount? FindById(long id);
}
