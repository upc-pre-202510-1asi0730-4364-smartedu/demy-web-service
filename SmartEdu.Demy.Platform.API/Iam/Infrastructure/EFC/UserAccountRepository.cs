using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;

/**
* <summary>
    *     Implementación concreta del repositorio de usuarios
    * </summary>
    */
public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
{
    private readonly AppDbContext _context;

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
    
    public bool ExistsByUsername(string username)
    {
        return _context.Set<UserAccount>()
            .Any(u => u.Email == username);
    }

    public UserAccount? FindByUsername(string username)
    {
        return _context.Set<UserAccount>()
            .FirstOrDefault(u => u.Email == username);
    }

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