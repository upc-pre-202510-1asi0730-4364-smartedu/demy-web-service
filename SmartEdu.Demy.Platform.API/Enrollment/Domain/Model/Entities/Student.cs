using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;

public partial class Student
{
    public long Id { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Dni { get; private set; }
    public ESex Sex { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string Address { get; private set; }
    public string PhoneNumber { get; private set; }

    public Student()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = string.Empty;
        Sex = ESex.Male;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }

    public Student(string firstName, string lastName, string dni, ESex sex, DateTime? birthDate, string address, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Dni = dni;
        Sex = sex;
        BirthDate = birthDate;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}