using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.Rest.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User administration endpoints")]
public class UsersController(
    IUserAccountQueryService queryService,
    IUserAccountCommandService commandService) : ControllerBase
{
    [HttpPut("teachers/{id}")]
    [SwaggerOperation(Summary = "Update teacher", OperationId = "UpdateTeacher")]
    public async Task<IActionResult> UpdateTeacher(long id, [FromBody] UpdateTeacherResource request)
    {
        var user = await queryService.Handle(new GetUserAccountByIdQuery(id));
        if (user is null)
            return NotFound(new { message = "User not found" });

        if (user.Role != Role.TEACHER)
            return StatusCode(403, new { message = "Only teachers can be updated here" });

        // 🔧 Aquí es donde estaba el error
        var command = new UpdateTeacherCommand(id, request.FullName, request.Email, request.NewPassword);
        var updated = await commandService.Handle(command);

        var resource = UserAccountResourceFromEntityAssembler.ToResource(updated);

        return Ok(new
        {
            message = "Teacher updated successfully",
            user = resource
        });
    }

    [HttpDelete("teachers/{id}")]
    [SwaggerOperation(Summary = "Delete teacher", OperationId = "DeleteTeacher")]
    public async Task<IActionResult> DeleteTeacher(long id)
    {
        var user = await queryService.Handle(new GetUserAccountByIdQuery(id));
        if (user is null)
            return NotFound(new { message = "User not found" });

        if (user.Role != Role.TEACHER)
            return StatusCode(403, new { message = "Only teachers can be deleted here" });

        var command = new DeleteTeacherCommand(id);
        await commandService.Handle(command);

        return Ok(new { message = "Teacher deleted successfully" });
    }

    [AllowAnonymous]
    [HttpPost("admins/sign-up")]
    [SwaggerOperation(Summary = "Admin sign-up", OperationId = "SignUpAdmin")]
    public async Task<IActionResult> SignUpAdmin([FromBody] SignUpAdminResource request)
    {
        var command = new SignUpAdminCommand(request.FullName, request.Email, request.Password);
        var created = await commandService.Handle(command);

        var resource = UserAccountResourceFromEntityAssembler.ToResource(created);

        return Created(string.Empty, new
        {
            message = "Admin registered successfully",
            user = resource
        });
    }

    [AllowAnonymous]
    [HttpPost("teachers")]
    [SwaggerOperation(Summary = "Create new teacher", OperationId = "CreateTeacher")]
    public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherResource request)
    {
        var command = new CreateTeacherCommand(request.FullName, request.Email, request.Password);
        var created = await commandService.Handle(command);

        var resource = UserAccountResourceFromEntityAssembler.ToResource(created);

        return Created(string.Empty, new
        {
            message = "Teacher created successfully",
            user = resource
        });
    }


    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(Summary = "Sign in user (admin or teacher)", OperationId = "SignInUser")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource request)
    {
        try
        {
            var user = await queryService.GetByEmailAsync(request.Email);
            if (user == null) return Unauthorized(new { message = "User not found" });

            string role = user.Role.ToString().ToLower();
            (UserAccount userAccount, string token) result;

            switch (role)
            {
                case "admin":
                    var adminCommand = new SignInAdminCommand(request.Email, request.Password);
                    result = await commandService.Handle(adminCommand);
                    break;

                case "teacher":
                    var teacherCommand = new SignInTeacherCommand(request.Email, request.Password);
                    result = await commandService.Handle(teacherCommand);
                    break;

                default:
                    return BadRequest(new { message = "Unsupported user role." });
            }

            var resource = UserAccountResourceFromEntityAssembler.ToResource(result.userAccount);

            return Ok(new
            {
                message = $"{role} login successful",
                token = result.token,
                user = resource
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPut("reset-password")]
    [SwaggerOperation(Summary = "Reset user password", OperationId = "ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource request)
    {
        if (request.NewPassword != request.RepeatPassword)
            return BadRequest(new { message = "Passwords do not match" });

        var command = new ResetPasswordCommand(request.Email, request.NewPassword);
        await commandService.Handle(command);

        return Ok(new
        {
            message = "Password reset successfully",
            email = request.Email
        });
    }

    [HttpGet("teachers")]
    [SwaggerOperation(Summary = "Get all teachers", OperationId = "GetAllTeachers")]
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await queryService.FindTeachersAsync();

        var resources = teachers
            .Select(UserAccountResourceFromEntityAssembler.ToResource)
            .ToList();

        return Ok(new
        {
            message = "Teachers retrieved successfully",
            teachers = resources
        });
    }

}
