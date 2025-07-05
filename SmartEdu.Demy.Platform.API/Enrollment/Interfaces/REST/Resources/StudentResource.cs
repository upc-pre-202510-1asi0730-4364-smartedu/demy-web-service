namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record StudentResource(
    int Id,
    string Name,
    string Dni,
    string Sex,
    DateTime BirthDate,
    string Address,
    string PhoneNumber
);