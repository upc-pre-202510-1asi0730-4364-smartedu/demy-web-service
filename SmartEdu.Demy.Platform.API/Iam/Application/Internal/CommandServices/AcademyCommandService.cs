using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.CommandServices;

public sealed class AcademyCommandService : IAcademyCommandService
{
    private readonly IAcademyRepository _academyRepository;
    private readonly IUserAccountRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcademyCommandService(
        IAcademyRepository academyRepository,
        IUserAccountRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _academyRepository = academyRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Academy> Handle(CreateAcademyCommand command)
    {
        // Validar que el usuario exista
        if (!await _userRepository.ExistsByIdAsync(command.UserId))
            throw new Exception("User does not exist");

        // Validar que el RUC no exista
        if (await _academyRepository.ExistsByRucAsync(command.Ruc))
            throw new Exception("Academy with the same RUC already exists");

        var academy = new Academy(command.UserId, command.AcademyName, command.Ruc);

        await _academyRepository.AddAsync(academy);
        await _unitOfWork.CompleteAsync();

        return academy;
    }
}