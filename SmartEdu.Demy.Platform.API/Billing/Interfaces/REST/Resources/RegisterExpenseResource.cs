namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record RegisterExpenseResource(
    string Category,
    string Concept,
    string Method,
    string Currency,
    decimal Amount,
    DateTime PaidAt
);