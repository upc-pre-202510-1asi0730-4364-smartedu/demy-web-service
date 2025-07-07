using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.QueryServices;

/// <summary>
/// Application service responsible for handling queries related to user accounts,
/// including retrieval by ID, email, and filtering by role.
/// </summary>
public sealed class UserAccountQueryService : IUserAccountQueryService
{
    private readonly IUserAccountRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountQueryService"/> class.
    /// </summary>
    /// <param name="repository">The user account repository.</param>
    public UserAccountQueryService(IUserAccountRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a user account by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user ID.</param>
    /// <returns>The matching <see cref="UserAccount"/>, or null if not found.</returns>
    public async Task<UserAccount?> Handle(GetUserAccountByIdQuery query)
    {
        return await Task.FromResult(_repository.FindById(query.Id));
    }

    /// <summary>
    /// Retrieves all user accounts with the ADMIN role.
    /// </summary>
    /// <returns>A list of users with the ADMIN role.</returns>
    public async Task<IEnumerable<UserAccount>> FindAdminsAsync()
    {
        var all = await _repository.ListAsync(); // asegúrate que tu repositorio tiene este método
        return all.Where(u => u.Role == Role.ADMIN);
    }

    /// <summary>
    /// Retrieves all user accounts with the TEACHER role.
    /// </summary>
    /// <returns>A list of users with the TEACHER role.</returns>
    public async Task<IEnumerable<UserAccount>> FindTeachersAsync()
    {
        var all = await _repository.ListAsync();
        return all.Where(u => u.Role == Role.TEACHER);
    }
    
    /// <summary>
    /// Retrieves a user account by its email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>The matching <see cref="UserAccount"/>, or null if not found.</returns>
    public async Task<UserAccount?> GetByEmailAsync(string email)
    {
        return await _repository.FindByEmailAsync(email);
    }


}