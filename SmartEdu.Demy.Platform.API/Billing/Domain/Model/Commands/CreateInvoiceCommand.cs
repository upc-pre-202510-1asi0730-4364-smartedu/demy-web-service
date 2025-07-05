namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

public record CreateInvoiceCommand(
    string dni,
    decimal amount,
    string currency,
    DateOnly dueDate
);