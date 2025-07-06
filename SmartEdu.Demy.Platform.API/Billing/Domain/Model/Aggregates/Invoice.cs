using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;
using MyCurrency = SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects.Currency;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;

public partial class Invoice
{
    public int Id { get; }
    
    public Dni Dni { get; private set; }

    public string Name { get; private set; }
    
    public Money MonetaryAmount { get; private set; }
    
    public DateTime DueDate { get; private set; }
    
    public EInvoiceStatus Status { get; private set; }

    public decimal Amount => MonetaryAmount.Amount;

    public string Currency => MonetaryAmount.Currency.Code;

    private Invoice() {}

    public Invoice(string dni, string name, decimal amount, string currency, DateTime dueDate)
    {
        Dni = new Dni(dni);
        Name = name;
        MonetaryAmount = new Money(amount, MyCurrency.Of(currency));
        DueDate = dueDate;
        Status = EInvoiceStatus.Pending;
    }

    public bool IsPaid()
    {
        return Status == EInvoiceStatus.Paid;
    }

    public void MarkAsPaid()
    {
        Status = EInvoiceStatus.Paid;
    }
}