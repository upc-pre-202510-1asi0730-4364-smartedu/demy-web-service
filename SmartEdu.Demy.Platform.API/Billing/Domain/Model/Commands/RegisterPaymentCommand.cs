namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

public record RegisterPaymentCommand(
    int invoiceId,
    string method
);