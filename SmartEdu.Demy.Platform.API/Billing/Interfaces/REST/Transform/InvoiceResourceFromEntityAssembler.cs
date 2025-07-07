using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice entity)
    {
        return new InvoiceResource(
            entity.Id,
            entity.Dni.Value,
            entity.Name,
            entity.Amount,
            entity.Currency,
            entity.DueDate,
            entity.Status.ToString()
        );
    }
}