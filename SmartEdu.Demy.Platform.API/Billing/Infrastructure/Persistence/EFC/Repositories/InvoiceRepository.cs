using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Infrastructure.Persistence.EFC.Repositories;

public class InvoiceRepository(AppDbContext context) : BaseRepository<Invoice>(context), IInvoiceRepository
{
    public async Task<IEnumerable<Invoice>> FindAllByStudentIdAsync(long studentId)
    {
        return await Context.Set<Invoice>()
            .Where(i => i.StudentId == studentId)
            .ToListAsync();
    }
}