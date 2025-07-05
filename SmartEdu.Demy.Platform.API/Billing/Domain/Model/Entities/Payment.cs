using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;
using MyCurrency = SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects.Currency;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;

public class Payment
{
    public int Id { get; }

    public int InvoiceId { get; private set; }
    
    public Money MonetaryAmount { get; private set; }

    public EPaymentMethod Method { get; private set; }
    
    public DateTime PaidAt { get; private set; }

    public Payment(int invoiceId, decimal amount, string currency, string method, DateTime paidAt)
    {
        InvoiceId = invoiceId;
        MonetaryAmount = new Money(amount, MyCurrency.Of(currency));
        Method = Enum.Parse<EPaymentMethod>(method);
        PaidAt = paidAt;
    }
}