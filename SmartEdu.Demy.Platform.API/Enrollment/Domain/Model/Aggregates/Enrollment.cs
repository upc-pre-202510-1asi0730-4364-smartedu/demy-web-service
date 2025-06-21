using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates
{
    public partial class Enrollment
    {
        public int Id { get; private set; }

        public int StudentId { get; private set; }
        public int  PeriodId  { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public EEnrollmentStatus EnrollmentStatus { get; private set; }

        public EPaymentStatus    PaymentStatus    { get; private set; }

        protected Enrollment() { }

        // Manual ctor
        public Enrollment(
            int studentId,
            int  periodId,
            decimal   amount,
            string    currency,
            EEnrollmentStatus enrollmentStatus,
            EPaymentStatus    paymentStatus)
        {
            StudentId        = studentId;
            PeriodId         = periodId;
            Amount           = amount;
            Currency         = currency;
            EnrollmentStatus = enrollmentStatus;
            PaymentStatus    = paymentStatus;
        }

        // Ctor desde el comando
        public Enrollment(CreateEnrollmentCommand command)
            : this(
                command.StudentId,
                command.PeriodId,
                command.Amount,
                command.Currency,
                Enum.Parse<EEnrollmentStatus>(command.EnrollmentStatus, ignoreCase: true),
                Enum.Parse<EPaymentStatus>(command.PaymentStatus,    ignoreCase: true)
              )
        {
        }

        // UpdateInformation actualizado
        public Enrollment UpdateInformation(
            decimal   amount,
            string    currency,
            string    enrollmentStatus,
            string    paymentStatus)
        {
            Amount           = amount;
            Currency         = currency;
            EnrollmentStatus = Enum.Parse<EEnrollmentStatus>(enrollmentStatus, ignoreCase: true);
            PaymentStatus    = Enum.Parse<EPaymentStatus>(paymentStatus,    ignoreCase: true);
            return this;
        }
    }
}
