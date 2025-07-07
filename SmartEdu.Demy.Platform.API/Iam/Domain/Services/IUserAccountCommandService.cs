using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

/// <summary>
/// Contract for handling user account commands including authentication and user lifecycle management.
/// </summary>
public interface IUserAccountCommandService
{
    /// <summary>
    /// Registers a new admin user account.
    /// </summary>
    /// <param name="command">The sign-up command with full name, email, and password.</param>
    /// <returns>The created <see cref="UserAccount"/> instance.</returns>
    Task<UserAccount> Handle(SignUpAdminCommand command);
    /// <summary>
    /// Authenticates an admin user and returns a token upon success.
    /// </summary>
    /// <param name="command">The sign-in command with email and password.</param>
    /// <returns>A tuple with the authenticated user and a JWT token.</returns>
    Task<(UserAccount user, string token)> Handle(SignInAdminCommand command);
    /// <summary>
    /// Creates a new teacher user account.
    /// </summary>
    /// <param name="command">The command containing full name, email, and password.</param>
    /// <returns>The created <see cref="UserAccount"/> instance.</returns>
    Task<UserAccount> Handle(CreateTeacherCommand command);
    /// <summary>
    /// Updates the information of an existing teacher account.
    /// </summary>
    /// <param name="command">The command containing the ID, updated data, and optional new password.</param>
    /// <returns>The updated <see cref="UserAccount"/> instance.</returns>
    Task<UserAccount> Handle(UpdateTeacherCommand command);
    /// <summary>
    /// Deletes a teacher account based on the provided ID.
    /// </summary>
    /// <param name="command">The command containing the teacher's ID to be deleted.</param>
    Task Handle(DeleteTeacherCommand command);
    /// <summary>
    /// Authenticates a teacher and returns a token upon success.
    /// </summary>
    /// <param name="command">The sign-in command with email and password.</param>
    /// <returns>A tuple with the authenticated user and a JWT token.</returns>
    Task<(UserAccount user, string token)> Handle(SignInTeacherCommand command);
    /// <summary>
    /// Resets the password for a user account by email.
    /// </summary>
    /// <param name="command">The reset password command with email and new password.</param>
    Task Handle(ResetPasswordCommand command);
}