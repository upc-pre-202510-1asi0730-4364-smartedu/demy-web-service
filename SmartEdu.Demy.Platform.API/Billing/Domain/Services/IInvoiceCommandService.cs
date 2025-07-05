using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Services;

public interface IInvoiceCommandService
{
    Task<int> Handle(CreateInvoiceCommand command);
}