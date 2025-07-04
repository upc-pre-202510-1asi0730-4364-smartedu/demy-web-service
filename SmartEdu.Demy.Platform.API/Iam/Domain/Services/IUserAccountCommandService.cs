using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Services;

public interface IUserAccountCommandService
{
    UserAccount SignUpAdmin(SignUpAdminResource resource);
    public (UserAccount user, string token) SignInAdmin(SignInAdminResource r);
    UserAccount CreateTeacher(CreateTeacherResource resource);
    UserAccount UpdateTeacher(long id, UpdateTeacherResource resource);
    UserAccount UpdateAdmin(long id, UpdateAdminResource resource);
    void DeleteTeacher(long id);
    public (UserAccount user, string token) SignInTeacher(SignInTeacherResource r);
    void ResetPassword(ResetPasswordResource resource);
}