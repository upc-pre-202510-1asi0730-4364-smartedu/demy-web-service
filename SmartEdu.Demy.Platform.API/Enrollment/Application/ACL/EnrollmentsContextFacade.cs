using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.ACL;


namespace SmartEdu.Demy.Platform.API.Enrollment.Application.ACL;

public class EnrollmentsContextFacade(IStudentQueryService studentQueryService) : IEnrollmentsContextFacade
{
    /// <inheritdoc />
    public async Task<string> FetchStudentFullNameByDni(string dni)
    {
        var getStudentByDniQuery = new GetStudentByDniQuery(dni);
        var student = await studentQueryService.Handle(getStudentByDniQuery);
        return student?.Name.FullName ?? "";
    }
}