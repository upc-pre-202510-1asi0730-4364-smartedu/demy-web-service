using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<IEnumerable<Invoice>> FindByDniAsync(Dni dni);

    new void Update(Invoice invoice);
}