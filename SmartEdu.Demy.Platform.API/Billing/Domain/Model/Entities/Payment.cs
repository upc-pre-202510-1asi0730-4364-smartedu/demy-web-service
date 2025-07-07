using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;
using MyCurrency = SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects.Currency;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;

public class Payment
{
    public int Id { get; }

    public int? InvoiceId { get; private set; }
    
    public Money MonetaryAmount { get; private set; }

    public EPaymentMethod Method { get; private set; }
    
    public DateTime PaidAt { get; private set; }

    private Payment() {}

    public Payment(decimal amount, string currency, string method, DateTime paidAt, int? invoiceId = null)
    {
        InvoiceId = invoiceId;
        MonetaryAmount = new Money(amount, MyCurrency.Of(currency));
        Method = Enum.Parse<EPaymentMethod>(method, ignoreCase: true);
        PaidAt = paidAt;
    }
}