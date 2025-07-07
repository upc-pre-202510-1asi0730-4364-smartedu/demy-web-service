namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record CreateFinancialTransactionResource(
    string Type,
    string Category,
    string Concept,
    DateTime Date,
    CreatePaymentResource Payment
);