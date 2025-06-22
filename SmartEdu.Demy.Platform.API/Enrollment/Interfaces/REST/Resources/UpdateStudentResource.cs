namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record UpdateStudentResource(
    string FirstName,
    string LastName,
    string Dni,
    string Sex,
    DateTime BirthDate,
    string Address,
    string PhoneNumber
);
