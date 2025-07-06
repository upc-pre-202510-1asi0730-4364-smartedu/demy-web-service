using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class RegisterPaymentCommandFromResourceAssembler
{
    public static RegisterPaymentCommand ToCommandFromResource(int invoiceId, RegisterPaymentResource resource)
    {
        return new RegisterPaymentCommand(
            invoiceId,
            resource.Method
        );
    }
}