namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.ACL;

public interface IEnrollmentsContextFacade
{
    Task<string> FetchStudentFullNameByDni(string dni);
}