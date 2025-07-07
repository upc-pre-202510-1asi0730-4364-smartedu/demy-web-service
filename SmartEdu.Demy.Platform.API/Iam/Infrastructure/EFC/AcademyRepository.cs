using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.EFC;

/// <summary>
/// Implementation of the <see cref="IAcademyRepository"/> using Entity Framework Core.
/// </summary>
public class AcademyRepository : BaseRepository<Academy>, IAcademyRepository
{
    private readonly AppDbContext _context;
    /// <summary>
    /// Initializes a new instance of the <see cref="AcademyRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public AcademyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    /// <summary>
    /// Checks if an academy with the given ID exists.
    /// </summary>
    /// <param name="id">The ID of the academy.</param>
    /// <returns><c>true</c> if it exists; otherwise, <c>false</c>.</returns>
    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.Id == id);
    }
    
    /// <summary>
    /// Checks if an academy with the specified RUC already exists.
    /// </summary>
    /// <param name="ruc">The RUC of the academy.</param>
    /// <returns><c>true</c> if it exists; otherwise, <c>false</c>.</returns>
    public async Task<bool> ExistsByRucAsync(string ruc)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.Ruc == ruc);
    }

    /// <summary>
    /// Checks if an academy already exists for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns><c>true</c> if an academy is registered to the user; otherwise, <c>false</c>.</returns>
    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _context.Set<Academy>().AnyAsync(a => a.UserId == userId);
    }
    
    /// <summary>
    /// Finds an academy by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the academy.</param>
    /// <returns>The <see cref="Academy"/> if found; otherwise, <c>null</c>.</returns>
    public async Task<Academy?> FindByIdAsync(long id)
    {
        return await _context.Set<Academy>().FindAsync(id);
    }
    /// <summary>
    /// Finds an academy by its RUC.
    /// </summary>
    /// <param name="ruc">The RUC of the academy.</param>
    /// <returns>The <see cref="Academy"/> if found; otherwise, <c>null</c>.</returns>
    public async Task<Academy?> FindByRucAsync(string ruc)
    {
        return await _context.Set<Academy>().FirstOrDefaultAsync(a => a.Ruc == ruc);
    }
}