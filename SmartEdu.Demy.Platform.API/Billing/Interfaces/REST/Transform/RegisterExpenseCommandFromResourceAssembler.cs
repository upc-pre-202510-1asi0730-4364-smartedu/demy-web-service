using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class RegisterExpenseCommandFromResourceAssembler
{
    public static RegisterExpenseCommand ToCommandFromResource(RegisterExpenseResource resource)
    {
        return new RegisterExpenseCommand(
            resource.Category,
            resource.Concept,
            resource.Method,
            resource.Currency,
            resource.Amount,
            resource.PaidAt
        );
    }
}