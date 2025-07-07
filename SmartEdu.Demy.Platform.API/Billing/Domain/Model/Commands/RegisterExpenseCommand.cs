namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

public record RegisterExpenseCommand(
    string category,
    string concept,
    string method,
    string currency,
    decimal amount,
    DateTime paidAt
);