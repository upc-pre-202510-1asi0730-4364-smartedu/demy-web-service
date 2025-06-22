using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

public record CreateStudentCommand(string FirstName, string LastName, string Dni, string Sex, DateTime BirthDate, string Address, string PhoneNumber); 