using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;

/// <summary>
/// Aggregate root that represents a student, including name, identification,
/// sex, birth date, address, and phone number.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public partial class Student
{
    /// <summary>
    /// Gets the unique identifier for the student.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets the student's full name.
    /// </summary>
    public PersonName Name { get; private set; }

    /// <summary>
    /// Gets the student's DNI (national identification number).
    /// </summary>
    public Dni Dni { get; private set; }

    /// <summary>
    /// Gets the student's sex.
    /// </summary>
    public ESex Sex { get; private set; }

    /// <summary>
    /// Gets the student's birth date.
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Gets the student's address.
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    /// Gets the student's phone number.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; }

    /// <summary>
    /// Required by EF Core.
    /// </summary>
    private Student() { }

    /// <summary>
    /// Creates a new Student instance with the specified data.
    /// </summary>
    /// <param name="firstName">First name</param>
    /// <param name="lastName">Last name</param>
    /// <param name="dni">DNI (national ID)</param>
    /// <param name="sex">Sex enum</param>
    /// <param name="birthDate">Birth date</param>
    /// <param name="address">Address</param>
    /// <param name="phoneNumber">Phone number</param>
    public Student(string firstName, string lastName, string dni, ESex sex, DateTime birthDate, string address, string phoneNumber)
    {
        Name = new PersonName(firstName, lastName);
        Dni = new Dni(dni);
        Sex = sex;
        BirthDate = birthDate;
        Address = address;
        PhoneNumber = new PhoneNumber(phoneNumber);
    }

    /// <summary>
    /// Creates a new Student instance from a CreateStudentCommand.
    /// </summary>
    /// <param name="command">The creation command with student data</param>
    public Student(CreateStudentCommand command)
        : this(
            command.FirstName,
            command.LastName,
            command.Dni,
            ParseSex(command.Sex),
            command.BirthDate,
            command.Address,
            command.PhoneNumber)
    {
    }

    /// <summary>
    /// Converts a string value to the ESex enum.
    /// Throws if the value is invalid.
    /// </summary>
    /// <param name="sexValue">String value to parse</param>
    /// <returns>The parsed ESex enum</returns>
    private static ESex ParseSex(string sexValue)
    {
        if (Enum.TryParse<ESex>(sexValue, ignoreCase: true, out var sex))
            return sex;
        throw new ArgumentException($"Invalid sex value: '{sexValue}'", nameof(sexValue));
    }

    /// <summary>
    /// Updates the student's information.
    /// </summary>
    /// <param name="firstName">New first name</param>
    /// <param name="lastName">New last name</param>
    /// <param name="dni">New DNI</param>
    /// <param name="sex">New sex enum</param>
    /// <param name="birthDate">New birth date</param>
    /// <param name="address">New address</param>
    /// <param name="phoneNumber">New phone number</param>
    /// <returns>The updated Student instance</returns>
    public Student UpdateInformation(
        string firstName,
        string lastName,
        string dni,
        ESex sex,
        DateTime birthDate,
        string address,
        string phoneNumber)
    {
        Name = new PersonName(firstName, lastName);
        Dni = new Dni(dni);
        Sex = sex;
        BirthDate = birthDate;
        Address = address;
        PhoneNumber = new PhoneNumber(phoneNumber);
        return this;
    }
}
