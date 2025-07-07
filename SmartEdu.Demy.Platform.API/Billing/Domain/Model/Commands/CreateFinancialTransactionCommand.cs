namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

public record CreateFinancialTransactionCommand(
    string type,
    string category,
    string concept,
    DateTime date,
    string method,
    string currency,
    decimal amount,
    DateTime paidAt,
    int invoiceId
);