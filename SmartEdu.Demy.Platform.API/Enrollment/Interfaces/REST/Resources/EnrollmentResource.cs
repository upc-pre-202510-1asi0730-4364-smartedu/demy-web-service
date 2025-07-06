namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record EnrollmentResource(int Id, int StudentId, int AcademicPeriodId, decimal Amount, string Currency, string EnrollmentStatus, string PaymentStatus);
