namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

public record InvoiceResource(
    int Id,
    string Dni,
    string Name,
    decimal Amount,
    string Currency,
    DateOnly DueDate,
    string Status
);