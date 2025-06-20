using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

public interface IUserAccountCommandService
{
    UserAccount SignUpAdmin(SignUpAdminResource resource);
    UserAccount SignInAdmin(SignInAdminResource resource);
    UserAccount CreateTeacher(CreateTeacherResource resource);
    UserAccount UpdateTeacher(long id, UpdateTeacherResource resource);
    UserAccount UpdateAdmin(long id, UpdateAdminResource resource);
    void DeleteTeacher(long id);
    UserAccount SignInTeacher(SignInTeacherResource resource);
    void ResetPassword(ResetPasswordResource resource);
}