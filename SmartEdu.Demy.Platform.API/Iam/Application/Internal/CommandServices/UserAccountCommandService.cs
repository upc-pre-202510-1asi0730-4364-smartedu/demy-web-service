using SmartEdu.Demy.Platform.API.Iam.Application.Internal.OutboundServices;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Persistence;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.CommandServices;

public sealed class UserAccountCommandService : IUserAccountCommandService
{
    private readonly IUserAccountRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public UserAccountCommandService(
        IUserAccountRepository userRepository,
        IHashingService hashingService,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserAccount> Handle(SignUpAdminCommand command)
    {
        if (await _userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("Email already registered");

        var hashedPassword = _hashingService.HashPassword(command.Password);
        var user = new UserAccount(0, command.FullName, command.Email, hashedPassword, Role.ADMIN, AccountStatus.ACTIVE);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }
    public async Task<(UserAccount user, string token)> Handle(SignInAdminCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null || user.Role != Role.ADMIN)
            throw new Exception("Invalid credentials");

        if (!_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return (user, token);
    }
    public async Task<UserAccount> Handle(CreateTeacherCommand command)
    {
        if (await _userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("Email already registered");

        var hashedPassword = _hashingService.HashPassword(command.Password);
        var user = new UserAccount(0, command.FullName, command.Email, hashedPassword, Role.TEACHER, AccountStatus.ACTIVE);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }
    public async Task<UserAccount> Handle(UpdateTeacherCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null || user.Role != Role.TEACHER)
            throw new Exception("Teacher not found");

        user.FullName = command.FullName;
        user.UpdateEmail(command.Email);
        if (!string.IsNullOrWhiteSpace(command.NewPassword))
        {
            var hashedPassword = _hashingService.HashPassword(command.NewPassword);
            user.ChangePassword(hashedPassword);
        }

        await _unitOfWork.CompleteAsync();
        return user;
    }

    public async Task Handle(DeleteTeacherCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null || user.Role != Role.TEACHER)
            throw new Exception("Teacher not found");

        _userRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<(UserAccount user, string token)> Handle(SignInTeacherCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null || user.Role != Role.TEACHER)
            throw new Exception("Invalid credentials");

        if (!_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return (user, token);
    }
    public async Task Handle(ResetPasswordCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null)
            throw new Exception("User not found");

        var hashedPassword = _hashingService.HashPassword(command.NewPassword);
        user.ChangePassword(hashedPassword);

        await _unitOfWork.CompleteAsync();
    }



}


