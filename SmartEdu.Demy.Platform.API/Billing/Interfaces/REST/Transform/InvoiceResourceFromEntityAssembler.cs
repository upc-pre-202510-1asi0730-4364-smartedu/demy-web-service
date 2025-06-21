using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice entity)
    {
        return new InvoiceResource(
            entity.Id,
            entity.StudentId,
            entity.Amount,
            entity.Currency,
            entity.DueDate,
            entity.Status.ToString(),
            entity.Payments.Select(p => new PaymentResource(
                p.Id,
                p.Amount,
                p.Currency,
                p.Method.ToString(),
                p.PaidAt
            )).ToList()
        );
    }
}