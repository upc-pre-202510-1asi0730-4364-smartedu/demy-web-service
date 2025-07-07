using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.ACL;

namespace SmartEdu.Demy.Platform.API.Billing.Application.Internal.OutboundServices.ACL;

public class ExternalEnrollmentsService(IEnrollmentsContextFacade enrollmentsContextFacade)
{
    public async Task<string> FetchStudentNameByDni(string dni)
    {
        var studentName = await enrollmentsContextFacade.FetchStudentFullNameByDni(dni);
        return string.IsNullOrWhiteSpace(studentName) ? "" : studentName;
    }
}