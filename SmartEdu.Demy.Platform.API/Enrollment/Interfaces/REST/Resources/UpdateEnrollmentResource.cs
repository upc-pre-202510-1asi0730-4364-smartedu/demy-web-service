namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record UpdateEnrollmentResource(decimal Amount, string Currency, string EnrollmentStatus, string PaymentStatus);