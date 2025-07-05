namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;

public record ResetPasswordCommand(string Email, string NewPassword);
