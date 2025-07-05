namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

public record UpdateStudentCommand(
    int    StudentId,
    string FirstName,
    string LastName,
    string Dni,
    string Sex,
    DateTime BirthDate,
    string Address,
    string PhoneNumber
);