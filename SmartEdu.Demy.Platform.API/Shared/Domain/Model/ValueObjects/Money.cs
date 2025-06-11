namespace SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing a monetary value with currency.
/// </summary>
public sealed record Money
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    
    public Money() : this(0m, Currency.Of("USD")) {}

    public Money(decimal amount, Currency currency)
    {
        if (currency is null)
            throw new ArgumentNullException(nameof(currency));
        
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        
        Amount = amount;
        Currency = currency;
    }
    
    public static Money Zero(Currency currency) => new (0m, currency);

    public Money Add(Money other)
    {
        RequireSameCurrency(other);
        return new Money(this.Amount + other.Amount, this.Currency);
    }

    public Money Subtract(Money other)
    {
        RequireSameCurrency(other);
        var result = this.Amount - other.Amount;
        if (result < 0)
            throw new InvalidOperationException("Resulting amount cannot be negative");
        return new Money(result, this.Currency);
    }

    public bool IsGreaterThanOrEqual(Money other)
    {
        RequireSameCurrency(other);
        return this.Amount > other.Amount;
    }
    
    public bool IsZero() => this.Amount == 0;

    private void RequireSameCurrency(Money other)
    {
        if (!this.Currency.Equals(other.Currency))
            throw new InvalidOperationException($"Currency mismatch: {this.Currency}, {other.Currency}");
    }
}