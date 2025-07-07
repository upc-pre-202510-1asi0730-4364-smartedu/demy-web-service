using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Billing.Application.Internal.QueryServices;

public class InvoiceQueryService(IInvoiceRepository invoiceRepository) : IInvoiceQueryService
{
    public async Task<Invoice?> Handle(GetInvoiceByIdQuery query)
    {
        return await invoiceRepository.FindByIdAsync(query.invoiceId);
    }
    public async Task<IEnumerable<Invoice>> Handle(GetAllInvoicesByDniQuery query)
    {
        return await invoiceRepository.FindByDniAsync(query.dni);
    }
}