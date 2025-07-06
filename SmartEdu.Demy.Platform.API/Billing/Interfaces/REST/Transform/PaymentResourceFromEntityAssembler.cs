using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity), "Payment entity is null");

        if (entity.MonetaryAmount is null)
            throw new NullReferenceException("Payment.MonetaryAmount is null");

        if (entity.MonetaryAmount.Currency is null)
            throw new NullReferenceException("Payment.MonetaryAmount.Currency is null");

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