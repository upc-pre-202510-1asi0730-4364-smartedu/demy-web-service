using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.ACL;
namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.OutboundServices.ACL;

public class ExternalEnrollmentServiceForAttendance(IEnrollmentsContextFacade enrollmentsContextFacade)
{
    public async Task<string> FetchStudentNameByDni(string dni)
    {
        var studentName = await enrollmentsContextFacade.FetchStudentFullNameByDni(dni);
        return string.IsNullOrWhiteSpace(studentName) ? "" : studentName;
    }
}