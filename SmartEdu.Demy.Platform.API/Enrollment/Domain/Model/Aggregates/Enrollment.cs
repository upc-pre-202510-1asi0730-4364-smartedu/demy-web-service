using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates
{
    /// <summary>
    /// Aggregate root that represents an enrollment, including
    /// student, period, schedule, payment, and status information.
    /// </summary>
    /// <remarks>Paul Sulca</remarks>
    public partial class Enrollment
    {
        /// <summary>
        /// Gets the unique identifier for the enrollment.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the ID of the student associated with the enrollment.
        /// </summary>
        public int StudentId { get; private set; }

        /// <summary>
        /// Gets the ID of the academic period associated with the enrollment.
        /// </summary>
        public int PeriodId { get; private set; }

        /// <summary>
        /// Gets the ID of the weekly schedule associated with the enrollment.
        /// </summary>
        public int WeeklyScheduleId { get; private set; }

        /// <summary>
        /// Gets the amount charged for the enrollment.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the currency in which the amount is charged.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Gets the enrollment status.
        /// </summary>
        public EEnrollmentStatus EnrollmentStatus { get; private set; }

        /// <summary>
        /// Gets the payment status.
        /// </summary>
        public EPaymentStatus PaymentStatus { get; private set; }

        /// <summary>
        /// Required by EF Core.
        /// </summary>
        protected Enrollment() { }

        /// <summary>
        /// Creates a new Enrollment instance with the specified data.
        /// </summary>
        /// <param name="studentId">ID of the student</param>
        /// <param name="periodId">ID of the academic period</param>
        /// <param name="weeklyScheduleId">ID of the weekly schedule</param>
        /// <param name="amount">Amount charged</param>
        /// <param name="currency">Currency of the amount</param>
        /// <param name="enrollmentStatus">Enrollment status enum</param>
        /// <param name="paymentStatus">Payment status enum</param>
        public Enrollment(
            int studentId,
            int periodId,
            int weeklyScheduleId,
            decimal amount,
            string currency,
            EEnrollmentStatus enrollmentStatus,
            EPaymentStatus paymentStatus)
        {
            StudentId = studentId;
            PeriodId = periodId;
            WeeklyScheduleId = weeklyScheduleId;
            Amount = amount;
            Currency = currency;
            EnrollmentStatus = enrollmentStatus;
            PaymentStatus = paymentStatus;
        }

        /// <summary>
        /// Updates the enrollment's financial and status information.
        /// </summary>
        /// <param name="amount">New amount</param>
        /// <param name="currency">New currency</param>
        /// <param name="enrollmentStatus">New enrollment status as string</param>
        /// <param name="paymentStatus">New payment status as string</param>
        /// <returns>The updated Enrollment instance</returns>
        public Enrollment UpdateInformation(
            decimal amount,
            string currency,
            string enrollmentStatus,
            string paymentStatus)
        {
            Amount = amount;
            Currency = currency;
            EnrollmentStatus = Enum.Parse<EEnrollmentStatus>(enrollmentStatus, ignoreCase: true);
            PaymentStatus = Enum.Parse<EPaymentStatus>(paymentStatus, ignoreCase: true);
            return this;
        }
    }
}
