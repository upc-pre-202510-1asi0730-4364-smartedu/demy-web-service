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
    /// <summary>
    /// Updates a teacher account.
    /// </summary>
    [HttpPut("teachers/{id}")]
    [SwaggerOperation(Summary = "Update teacher", OperationId = "UpdateTeacher")]
    [SwaggerResponse(StatusCodes.Status200OK, "Teacher updated successfully")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User is not a teacher")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
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

    /// <summary>
    /// Deletes a teacher account.
    /// </summary>
    [HttpDelete("teachers/{id}")]
    [SwaggerOperation(Summary = "Delete teacher", OperationId = "DeleteTeacher")]
    [SwaggerResponse(StatusCodes.Status200OK, "Teacher deleted successfully")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User is not a teacher")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
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

    /// <summary>
    /// Registers a new administrator account.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("admins/sign-up")]
    [SwaggerOperation(Summary = "Admin sign-up", OperationId = "SignUpAdmin")]
    [SwaggerResponse(StatusCodes.Status201Created, "Admin registered successfully")]
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

    /// <summary>
    /// Creates a new teacher account.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("teachers")]
    [SwaggerOperation(Summary = "Create new teacher", OperationId = "CreateTeacher")]
    [SwaggerResponse(StatusCodes.Status201Created, "Teacher created successfully")]
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


    /// <summary>
    /// Authenticates an admin or teacher and returns a JWT token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(Summary = "Sign in user (admin or teacher)", OperationId = "SignInUser")]
    [SwaggerResponse(StatusCodes.Status200OK, "Login successful")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unsupported user role")]
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

    /// <summary>
    /// Resets the password of a user account by email.
    /// </summary>
    [HttpPut("reset-password")]
    [SwaggerOperation(Summary = "Reset user password", OperationId = "ResetPassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Email or password missing")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewPassword))
            return BadRequest(new { message = "Email and password are required." });

        var command = new ResetPasswordCommand(request.Email, request.NewPassword);
        await commandService.Handle(command);

        return Ok(new
        {
            message = "Password reset successfully",
            email = request.Email
        });
    }
    
    /// <summary>
    /// Retrieves all registered teachers.
    /// </summary>
    [HttpGet("teachers")]
    [SwaggerOperation(Summary = "Get all teachers", OperationId = "GetAllTeachers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Teachers retrieved successfully")]
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
