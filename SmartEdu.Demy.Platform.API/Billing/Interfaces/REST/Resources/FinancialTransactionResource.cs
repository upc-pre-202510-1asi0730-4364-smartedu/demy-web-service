namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record FinancialTransactionResource(
    int Id,
    string Type,
    string Category,
    string Concept,
    DateTime Date,
    PaymentResource Payment
);