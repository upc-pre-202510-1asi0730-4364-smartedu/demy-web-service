using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;

public partial class Student
{
    public int Id { get; set; }
    public PersonName Name{ get; private set; }
    public Dni Dni { get; private set; }    
    public ESex Sex { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    private Student() { }   

    
    public Student(string firstName, string lastName, string dni, ESex sex, DateTime birthDate, string address, string phoneNumber)
    {
        Name = new PersonName(firstName, lastName);
        Dni = new Dni(dni);
        Sex = sex;
        BirthDate = birthDate;
        Address = address;
        PhoneNumber = new PhoneNumber(phoneNumber);
    }

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
    private static ESex ParseSex(string sexValue)
    {
        if (Enum.TryParse<ESex>(sexValue, ignoreCase: true, out var sex))
            return sex;
        throw new ArgumentException($"Valor de sexo inválido: '{sexValue}'", nameof(sexValue));
    }
    
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