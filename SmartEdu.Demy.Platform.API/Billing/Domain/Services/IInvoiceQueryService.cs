using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Services;

public interface IInvoiceQueryService
{
    Task<Invoice?> Handle(GetInvoiceByIdQuery query);
    Task<IEnumerable<Invoice>> Handle(GetAllInvoicesByDniQuery query);
}