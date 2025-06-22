using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SmartEdu.Demy.Platform.API.Enrollment.Infrastructure.Persistence.EFC.Repositories;

public class AcademicPeriodRepository(AppDbContext context)
    : BaseRepository<AcademicPeriod>(context), IAcademicPeriodRepository
{
    public async Task<bool> ExistsByPeriodNameAsync(string periodName)
    {
        return await context.Set<AcademicPeriod>().AnyAsync(ap => ap.PeriodName == periodName);
    }

    public async Task<AcademicPeriod?> FindByPeriodNameAsync(string periodName)
    {
        return await context.Set<AcademicPeriod>()
            .FirstOrDefaultAsync(ap => ap.PeriodName == periodName);
    }

    public async Task<bool> ExistsByPeriodNameAndIdIsNotAsync(string periodName, int id)
    {
        return await context.Set<AcademicPeriod>()
            .AnyAsync(ap => ap.PeriodName == periodName && ap.Id != id);
    }
}