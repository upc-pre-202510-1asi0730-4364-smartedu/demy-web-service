namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record PaymentResource(
        long Id,
        decimal Amount,
        string Currency,
        string Method,
        DateTime PaidAt
    );