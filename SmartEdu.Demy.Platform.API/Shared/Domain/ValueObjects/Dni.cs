namespace SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects
{
    /// <summary>
    /// Value object para un DNI
    /// </summary>
    public record Dni
    {
        public string Value { get; }
        
        private Dni()
        {
            Value = string.Empty;
        }        
        public Dni(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("DNI no puede estar vacío", nameof(value));
            if (value.Length != 8 || !value.All(char.IsDigit))
                throw new ArgumentException("Formato inválido: el DNI debe tener 8 dígitos", nameof(value));

            Value = value;
        }
        
        public override string ToString() => Value;
    }
}