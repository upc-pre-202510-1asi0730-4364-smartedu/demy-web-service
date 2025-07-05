using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;

/**
* <summary>
    *     Implementación concreta del repositorio de usuarios
    * </summary>
    */
public class UserRepository : BaseRepository<UserAccount>, IUserAccountRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserAccount?> FindByUsernameAsync(string username)
    {
        // Usamos Email como identificador, porque NO tienes Username en tu clase
        return await _context.Set<UserAccount>()
            .FirstOrDefaultAsync(u => u.Email == username);
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
}