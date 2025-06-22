using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Infrastructure.Persistence.EFC.Repositories;

public class EnrollmentRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregates.Enrollment>(context), IEnrollmentRepository
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> FindAllByStudentIdAsync(int studentId)
    {
        return await context.Set<Domain.Model.Aggregates.Enrollment>()
            .Where(e => e.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Enrollment>> FindAllByPeriodIdAsync(int periodId)
    {
        return await context.Set<Domain.Model.Aggregates.Enrollment>()
            .Where(e => e.PeriodId == periodId)
            .ToListAsync();
    }

    public async Task<Domain.Model.Aggregates.Enrollment?> FindByStudentIdAndPeriodIdAsync(int studentId, int periodId)
    {
        return await context.Set<Domain.Model.Aggregates.Enrollment>()
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.PeriodId == periodId);
    }
}