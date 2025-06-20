using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SmartEdu.Demy.Platform.API.Iam.Application.Internal.CommandServices;

public sealed class UserAccountCommandService : IUserAccountCommandService
{
    private static string Hash(string p) => $"hashed-{p}";

    private readonly AppDbContext _context;
    public UserAccountCommandService(AppDbContext context) => _context = context;

    // ---------- ADMINS ----------
    public UserAccount SignUpAdmin(SignUpAdminResource r)
    {
        if (_context.UserAccounts.Any(u => u.Email == r.Email))
            throw new Exception("Email already registered");

        var user = new UserAccount(0, r.FullName, r.Email, Hash(r.Password), Role.ADMIN);
        _context.UserAccounts.Add(user);
        _context.SaveChanges();
        return user;
    }

    public UserAccount SignInAdmin(SignInAdminResource r)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.Email == r.Email);
        if (user == null || user.Role != Role.ADMIN)
            throw new Exception("Invalid credentials");

        return user;
    }

    public UserAccount UpdateAdmin(long id, UpdateAdminResource r)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == id);
        if (user == null)
            throw new Exception("User not found");

        if (user.Role != Role.ADMIN)
            throw new Exception("Only admins can be updated here");

        user.FullName = r.FullName;
        user.UpdateEmail(r.Email);

        _context.SaveChanges();
        return user;
    }

    // ---------- TEACHERS ----------
    public UserAccount CreateTeacher(CreateTeacherResource r)
    {
        if (_context.UserAccounts.Any(u => u.Email == r.Email))
            throw new Exception("Email already registered");

        var user = new UserAccount(0, r.FullName, r.Email, Hash(r.Password), Role.TEACHER);
        _context.UserAccounts.Add(user);
        _context.SaveChanges();
        return user;
    }

    public UserAccount SignInTeacher(SignInTeacherResource r)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.Email == r.Email);
        if (user == null || user.Role != Role.TEACHER)
            throw new Exception("Invalid credentials");

        return user;
    }

    public UserAccount UpdateTeacher(long id, UpdateTeacherResource r)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == id);
        if (user == null)
            throw new Exception("User not found");

        if (user.Role != Role.TEACHER)
            throw new Exception("Only teachers can be updated here");

        user.FullName = r.FullName;
        user.UpdateEmail(r.Email);
        user.ChangePassword(Hash(r.NewPassword));

        _context.SaveChanges();
        return user;
    }

    public void DeleteTeacher(long id)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == id && u.Role == Role.TEACHER);
        if (user == null)
            throw new Exception("Teacher not found");

        _context.UserAccounts.Remove(user);
        _context.SaveChanges();
    }

    public void ResetPassword(ResetPasswordResource r)
    {
        var user = _context.UserAccounts.FirstOrDefault(u => u.Email == r.Email);
        if (user == null)
            throw new Exception("User not found");

        user.ChangePassword(Hash(r.NewPassword));
        _context.SaveChanges();
    }
}

