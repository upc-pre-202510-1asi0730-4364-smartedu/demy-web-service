using System.ComponentModel.DataAnnotations.Schema;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;

public class Payment
{
    public long Id { get; }
    
    public decimal Amount { get; private set; }

    public string Currency { get; private set; } = "PEN";
    
    public EPaymentMethod Method { get; private set; }
    
    public DateTime PaidAt { get; private set; }
    
    public long InvoiceId { get; private set; }
    
    public Invoice Invoice { get; private set; }

    public Payment(decimal amount, string currency, EPaymentMethod method, DateTime paidAt)
    {
        if (amount < 0) throw new ArgumentException("Amount must be non-negative", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));
        
        Amount = amount;
        Currency = currency;
        Method = method;
        PaidAt = paidAt;
    }

    public void AssignToInvoice(Invoice invoice)
    {
        Invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
        InvoiceId = invoice.Id;
    }
}