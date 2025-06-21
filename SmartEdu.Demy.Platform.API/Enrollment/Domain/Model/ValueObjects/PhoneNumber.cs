namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; }
        
        public PhoneNumber() { Value = string.Empty; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty.", nameof(value));

            // Validar el número de teléfono (por ejemplo, que solo contenga números)
            if (!IsValidPhoneNumber(value))
                throw new ArgumentException("Invalid phone number format.", nameof(value));

            Value = value;
        }

        private bool IsValidPhoneNumber(string value)
        {
            // Aquí agregas la lógica de validación (por ejemplo, solo números)
            return value.All(char.IsDigit) && value.Length == 9; 
        }

        public override string ToString() => Value;
    }
}