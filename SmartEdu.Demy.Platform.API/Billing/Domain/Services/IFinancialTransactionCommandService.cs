using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Services;

public interface IFinancialTransactionCommandService
{
    Task<FinancialTransaction?> Handle(CreateFinancialTransactionCommand command);

    Task<FinancialTransaction?> Handle(RegisterPaymentCommand command);

    Task<FinancialTransaction?> Handle(RegisterExpenseCommand command);
}