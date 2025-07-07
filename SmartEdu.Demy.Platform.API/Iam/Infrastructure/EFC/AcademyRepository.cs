using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;

public class AcademyRepository : BaseRepository<Academy>, IAcademyRepository
{
    private readonly AppDbContext _context;

    public AcademyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.Id == id);
    }
    public async Task<bool> ExistsByRucAsync(string ruc)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.Ruc == ruc);
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.UserId == userId);
    }

    public async Task<Academy?> FindByIdAsync(long id)
    {
        return await _context.Set<Academy>().FindAsync(id);
    }

    public async Task<Academy?> FindByRucAsync(string ruc)
    {
        return await _context.Set<Academy>().FirstOrDefaultAsync(a => a.Ruc == ruc);
    }
}