using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

public interface IUserAccountCommandService
{
    Task<UserAccount> Handle(SignUpAdminCommand command);
    Task<(UserAccount user, string token)> Handle(SignInAdminCommand command);
    Task<UserAccount> Handle(CreateTeacherCommand command);
    Task<UserAccount> Handle(UpdateTeacherCommand command);
    Task Handle(DeleteTeacherCommand command);
    Task<(UserAccount user, string token)> Handle(SignInTeacherCommand command);
    Task Handle(ResetPasswordCommand command);
}