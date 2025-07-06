using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Billing.Application.Internal.QueryServices;

public class FinancialTransactionQueryService(IFinancialTransactionRepository financialTransactionRepository) : IFinancialTransactionQueryService
{
    public async Task<FinancialTransaction> Handle(GetFinancialTransactionByIdQuery query)
    {
        return await financialTransactionRepository.FindByIdAsync(query.FinancialTransactionId);
    }

    public async Task<IEnumerable<FinancialTransaction>> Handle(GetAllFinancialTransactionsQuery query)
    {
        return await financialTransactionRepository.ListAsync();
    }
}