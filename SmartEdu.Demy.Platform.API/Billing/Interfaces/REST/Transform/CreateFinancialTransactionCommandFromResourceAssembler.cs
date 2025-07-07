using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class CreateFinancialTransactionCommandFromResourceAssembler
{
    public static CreateFinancialTransactionCommand ToCommandFromResource(CreateFinancialTransactionResource resource)
    {
        return new CreateFinancialTransactionCommand(
            resource.Type,
            resource.Category,
            resource.Concept,
            resource.Date,
            resource.Payment.Method,
            resource.Payment.Currency,
            resource.Payment.Amount,
            resource.Payment.PaidAt,
            resource.Payment.InvoiceId
        );
    }
}