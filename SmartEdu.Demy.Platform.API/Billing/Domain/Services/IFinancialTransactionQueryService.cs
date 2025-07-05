using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Services;

public interface IFinancialTransactionQueryService
{
    Task<IEnumerable<FinancialTransaction>> Handle(GetAllFinancialTransactionsQuery query);
}