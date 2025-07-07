using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;

/// <summary>
/// Concrete implementation of the <see cref="IUserAccountRepository"/> using Entity Framework Core.
/// Provides access to <see cref="UserAccount"/> persistence operations.
/// </summary>
public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public UserAccountRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _context.Set<UserAccount>().AnyAsync(u => u.UserId == id);
    }
    
    public async Task<UserAccount?> FindByUsernameAsync(string username)
    {
        return await _context.Set<UserAccount>()
            .FirstOrDefaultAsync(u => u.Email == username);
    }

    public async Task<UserAccount?> FindByIdAsync(long id)
    {
        return await _context.Set<UserAccount>().FindAsync(id);
    }
    
    /// <summary>
    /// Checks if a user exists by their username (email) synchronously.
    /// </summary>
    /// <param name="username">The email to check.</param>
    /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
    public bool ExistsByUsername(string username)
    {
        return _context.Set<UserAccount>()
            .Any(u => u.Email == username);
    }

    /// <summary>
    /// Finds a user by their username (email) synchronously.
    /// </summary>
    /// <param name="username">The email of the user.</param>
    /// <returns>The <see cref="UserAccount"/> if found; otherwise, <c>null</c>.</returns>
    public UserAccount? FindByUsername(string username)
    {
        return _context.Set<UserAccount>()
            .FirstOrDefault(u => u.Email == username);
    }

    /// <summary>
    /// Finds a user by their ID synchronously.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The <see cref="UserAccount"/> if found; otherwise, <c>null</c>.</returns>
    public UserAccount? FindById(long id)
    {
        return _context.Set<UserAccount>().Find(id);
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Set<UserAccount>().AnyAsync(u => u.Email == email);
    }

    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        return await _context.Set<UserAccount>().FirstOrDefaultAsync(u => u.Email == email);
    }
    

}