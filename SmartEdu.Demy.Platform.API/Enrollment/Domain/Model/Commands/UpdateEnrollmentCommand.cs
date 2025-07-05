namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

public record UpdateEnrollmentCommand(
    int    EnrollmentId,
    decimal Amount,
    string Currency,
    string EnrollmentStatus,
    string PaymentStatus
);