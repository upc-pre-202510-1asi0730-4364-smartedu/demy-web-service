namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record InvoiceResource(
        long Id,
        long StudentId,
        decimal Amount,
        string Currency,
        DateTime DueDate,
        string Status,
        IEnumerable<PaymentResource> Payments
    );