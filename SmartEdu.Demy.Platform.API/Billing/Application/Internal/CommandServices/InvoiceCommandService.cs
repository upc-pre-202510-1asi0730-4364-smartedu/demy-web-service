using SmartEdu.Demy.Platform.API.Billing.Application.Internal.OutboundServices.ACL;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Application.Internal.CommandServices;

public class InvoiceCommandService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork,
    ExternalEnrollmentsService externalEnrollmentsService) : IInvoiceCommandService
{
    public async Task<int> Handle(CreateInvoiceCommand command)
    {
        var name = await externalEnrollmentsService.FetchStudentNameByDni(command.dni);
        if (string.IsNullOrEmpty(name)) throw new Exception($"Student not found with dni {command.dni}");

        var invoice = new Invoice(
            command.dni,
            name,
            command.amount,
            command.currency,
            command.dueDate
        );

        try
        {
            await invoiceRepository.AddAsync(invoice);
            await unitOfWork.CompleteAsync();
            return invoice.Id;
        }
        catch (Exception ex)
        {
            throw new  Exception($"An error occured while adding new invoice: {ex.Message}");
        }
    }
}