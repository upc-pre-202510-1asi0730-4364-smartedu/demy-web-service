using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.QueryServices;

public sealed class UserAccountQueryService : IUserAccountQueryService
{
    private readonly IUserAccountRepository _repository;

    public UserAccountQueryService(IUserAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserAccount?> FindByIdAsync(long id)
    {
        return await Task.FromResult(_repository.FindById(id)); // O usa directamente EF si es async
    }

    public async Task<IEnumerable<UserAccount>> FindAdminsAsync()
    {
        var all = await _repository.ListAsync(); // asegúrate que tu repositorio tiene este método
        return all.Where(u => u.Role == Role.ADMIN);
    }

    public async Task<IEnumerable<UserAccount>> FindTeachersAsync()
    {
        var all = await _repository.ListAsync();
        return all.Where(u => u.Role == Role.TEACHER);
    }
}