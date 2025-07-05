using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

public interface IUserAccountQueryService
{
    Task<UserAccount?> Handle(GetUserAccountByIdQuery query);
    Task<IEnumerable<UserAccount>> FindAdminsAsync();
    Task<IEnumerable<UserAccount>> FindTeachersAsync();
}