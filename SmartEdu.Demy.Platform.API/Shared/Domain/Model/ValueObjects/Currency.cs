namespace SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing a 3-letter ISO currency.
/// </summary>
public sealed record Currency
{
    public string Code { get; }

    private static readonly HashSet<string> ValidCodes = new(StringComparer.OrdinalIgnoreCase)
    {
        "USD", "EUR", "PEN", "JPY", "GBP", "BRL", "MXN", "ARS", "CLP"
    };

    private Currency(string code)
    {
        Code = code.ToUpperInvariant();
    }

    public static Currency Of(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
            throw new ArgumentException("Currency code must be a 3-letter string.", nameof(code));
        
        if (!ValidCodes.Contains(code.ToUpperInvariant()))
            throw new ArgumentException($"Currency code '{code}' is not supported.", nameof(code));

        return new Currency(code);
    }
    
    public override string ToString() => Code;
}