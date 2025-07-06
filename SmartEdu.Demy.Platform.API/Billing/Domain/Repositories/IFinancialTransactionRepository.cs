using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;

public interface IFinancialTransactionRepository : IBaseRepository<FinancialTransaction>
{
    Task<IEnumerable<FinancialTransaction>> ListWithPaymentAsync();

    Task<FinancialTransaction?> FindByIdWithPaymentAsync(int id);
}