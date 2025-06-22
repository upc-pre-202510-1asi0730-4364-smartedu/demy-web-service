namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record EnrollmentResource(int EnrollmentId, int StudentId, int AcademicPeriodId, decimal Amount, string Currency, string EnrollmentStatus, string PaymentStatus);
