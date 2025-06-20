
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

public interface IUserAccountQueryService
{
    Task<UserAccount?> FindByIdAsync(long id);
    Task<IEnumerable<UserAccount>> FindAdminsAsync();
    Task<IEnumerable<UserAccount>> FindTeachersAsync();
}