namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record StudentResource(
    long Id,
    string FirstName,
    string LastName,
    string Dni,
    string Sex,
    DateTime? BirthDate,
    string Address,
    string PhoneNumber,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);