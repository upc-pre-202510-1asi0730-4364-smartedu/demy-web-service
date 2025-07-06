using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Infrastructure.Persistence.EFC.Repositories;

public class FinancialTransactionRepository(AppDbContext context) : BaseRepository<FinancialTransaction>(context), IFinancialTransactionRepository
{
    // Método explícito para incluir Payment en List
    public async Task<IEnumerable<FinancialTransaction>> ListWithPaymentAsync()
    {
        return await Context.Set<FinancialTransaction>()
            .Include(ft => ft.Payment)
            .ToListAsync();
    }

    // Método explícito para incluir Payment en FindById
    public async Task<FinancialTransaction?> FindByIdWithPaymentAsync(int id)
    {
        return await Context.Set<FinancialTransaction>()
            .Include(ft => ft.Payment)
            .FirstOrDefaultAsync(ft => ft.Id == id);
    }
}