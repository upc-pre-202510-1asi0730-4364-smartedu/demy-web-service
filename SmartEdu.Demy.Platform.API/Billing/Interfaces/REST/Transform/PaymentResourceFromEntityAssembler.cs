using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.MonetaryAmount.Amount,
            entity.MonetaryAmount.Currency.Code,
            entity.Method.ToString(),
            entity.PaidAt,
            entity.InvoiceId
        );
    }
}