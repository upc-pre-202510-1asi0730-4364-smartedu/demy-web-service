namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record CreateInvoiceResource(
    decimal Amount,
    string Currency,
    DateTime DueDate
);