using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class FinancialTransactionResourceFromEntityAssembler
{
    public static FinancialTransactionResource ToResourceFromEntity(FinancialTransaction entity)
    {
        return new FinancialTransactionResource(
            entity.Id,
            entity.Type.ToString(),
            entity.Category.ToString(),
            entity.Concept,
            entity.Date,
            PaymentResourceFromEntityAssembler.ToResourceFromEntity(entity.Payment)
        );
    }
}