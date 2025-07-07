using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(string dni, CreateInvoiceResource resource)
    {
        return new CreateInvoiceCommand(
            dni,
            resource.Amount,
            resource.Currency,
            resource.DueDate
        );
    }
}