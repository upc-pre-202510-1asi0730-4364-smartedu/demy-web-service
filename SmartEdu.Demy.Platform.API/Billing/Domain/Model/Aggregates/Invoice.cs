using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;

public partial class Invoice
{
    public long Id { get; }
    
    public long StudentId { get; private set; }
    
    public decimal Amount { get; private set; }
    
    public string Currency { get; private set; }
    
    public DateTime DueDate { get; private set; }
    
    public EInvoiceStatus Status { get; private set; }
    
    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public Invoice(long studentId, decimal amount, string currency, DateTime dueDate)
    {
        if (amount < 0) throw new ArgumentException("Amount must be non-negative.");
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code.");

        StudentId = studentId;
        Amount = amount;
        Currency = currency.ToUpperInvariant();
        DueDate = dueDate;
        Status = EInvoiceStatus.Pending;
    }
    
    public void AddPayment(Payment payment)
    {
        if (payment == null) throw new ArgumentNullException(nameof(payment));
        payment.AssignToInvoice(this);
        _payments.Add(payment);

        if (IsPaid())
        {
            Status = EInvoiceStatus.Paid;
        }
    }

    public decimal GetPaidAmount()
    {
        return _payments
            .Where(p => p.Currency == this.Currency)
            .Sum(p => p.Amount);
    }

    public bool IsPaid()
    {
        return GetPaidAmount() >= Amount;
    }
}