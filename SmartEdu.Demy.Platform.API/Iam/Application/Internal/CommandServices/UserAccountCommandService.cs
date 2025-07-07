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


/// <summary>
/// Application service responsible for handling commands related to user account operations,
/// including admin and teacher registration, authentication, password reset, and deletion.
/// </summary>
public sealed class UserAccountCommandService : IUserAccountCommandService
{
    private readonly IUserAccountRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAccountCommandService"/> class.
    /// </summary>
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
    
    /// <summary>
    /// Handles the sign-up process for a new admin user.
    /// </summary>
    /// <param name="command">The sign-up command containing full name, email, and password.</param>
    /// <returns>The created <see cref="UserAccount"/>.</returns>
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
    
    /// <summary>
    /// Handles the sign-in process for an admin user.
    /// </summary>
    /// <param name="command">The sign-in command containing email and password.</param>
    /// <returns>A tuple with the authenticated user and a JWT token.</returns>
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
    
    /// <summary>
    /// Handles the creation of a new teacher account.
    /// </summary>
    /// <param name="command">The command containing teacher creation details.</param>
    /// <returns>The created <see cref="UserAccount"/>.</returns>
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
    /// <summary>
    /// Handles updating an existing teacher account.
    /// </summary>
    /// <param name="command">The update command containing the teacher's new information.</param>
    /// <returns>The updated <see cref="UserAccount"/>.</returns>
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

    /// <summary>
    /// Handles deletion of a teacher account.
    /// </summary>
    /// <param name="command">The command containing the teacher's ID to delete.</param>
    public async Task Handle(DeleteTeacherCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null || user.Role != Role.TEACHER)
            throw new Exception("Teacher not found");

        _userRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
    }
    /// <summary>
    /// Handles the sign-in process for a teacher account.
    /// </summary>
    /// <param name="command">The sign-in command with email and password.</param>
    /// <returns>A tuple with the authenticated user and a JWT token.</returns>
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
    
    /// <summary>
    /// Handles the reset of a user's password.
    /// </summary>
    /// <param name="command">The reset password command with email and new password.</param>
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


