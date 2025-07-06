using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.ACL;
namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.OutboundServices.ACL;

/// <summary>
/// ACL (Anti-Corruption Layer) service to interact with the Enrollment bounded context.
/// Provides methods for retrieving enrollment-related data required by the Attendance context.
/// </summary>
public class ExternalEnrollmentServiceForAttendance(IEnrollmentsContextFacade enrollmentsContextFacade)
{
    /// <summary>
    /// Fetches the full name of a student based on their DNI by delegating the call to the Enrollment context.
    /// </summary>
    /// <param name="dni">The DNI of the student.</param>
    /// <returns>The full name of the student, or an empty string if not found or null.</returns>
    public async Task<string> FetchStudentNameByDni(string dni)
    {
        var studentName = await enrollmentsContextFacade.FetchStudentFullNameByDni(dni);
        return string.IsNullOrWhiteSpace(studentName) ? "" : studentName;
    }
}