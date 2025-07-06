namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record CreatePaymentResource(
    string Method,
    string Currency,
    decimal Amount,
    DateTime PaidAt,
    int InvoiceId
);