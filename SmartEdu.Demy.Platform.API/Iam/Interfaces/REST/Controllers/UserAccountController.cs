using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User administration endpoints")]
public class UsersController(
    IUserAccountQueryService queryService,
    IUserAccountCommandService commandService) : ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user by ID", OperationId = "GetUserById")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await queryService.FindByIdAsync(id);
        if (user is null)
            return NotFound(new { message = "User not found" });

        var resource = UserAccountResourceFromEntityAssembler.ToResource(user);
        return Ok(resource);
    }

    [HttpGet("admins")]
    [SwaggerOperation(Summary = "Get all admins", OperationId = "GetAdmins")]
    public async Task<IActionResult> GetAdmins()
    {
        var admins = await queryService.FindAdminsAsync();
        var resources = admins.Select(UserAccountResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("teachers")]
    [SwaggerOperation(Summary = "Get all teachers", OperationId = "GetTeachers")]
    public async Task<IActionResult> GetTeachers()
    {
        var teachers = await queryService.FindTeachersAsync();
        var resources = teachers.Select(UserAccountResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPut("admins/{id}")]
    [SwaggerOperation(Summary = "Update admin", OperationId = "UpdateAdmin")]
    public async Task<IActionResult> UpdateAdmin(long id, [FromBody] UpdateAdminResource request)
    {
        var user = await queryService.FindByIdAsync(id);
        if (user is null)
            return NotFound(new { message = "User not found" });

        if (user.Role != Role.ADMIN)
            return StatusCode(403, new { message = "Only admins can be updated here" });

        var updated = commandService.UpdateAdmin(id, request);
        var resource = UserAccountResourceFromEntityAssembler.ToResource(updated);

        return Ok(new
        {
            message = "Admin updated successfully",
            user = resource
        });
    }

    [HttpPut("teachers/{id}")]
    [SwaggerOperation(Summary = "Update teacher", OperationId = "UpdateTeacher")]
    public async Task<IActionResult> UpdateTeacher(long id, [FromBody] UpdateTeacherResource request)
    {
        var user = await queryService.FindByIdAsync(id);
        if (user is null)
            return NotFound(new { message = "User not found" });

        if (user.Role != Role.TEACHER)
            return StatusCode(403, new { message = "Only teachers can be updated here" });

        var updated = commandService.UpdateTeacher(id, request);
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
        var user = await queryService.FindByIdAsync(id);
        if (user is null)
            return NotFound(new { message = "User not found" });

        if (user.Role != Role.TEACHER)
            return StatusCode(403, new { message = "Only teachers can be deleted here" });

        commandService.DeleteTeacher(id);

        return Ok(new { message = "Teacher deleted successfully" });
    }

    [HttpPost("admins/sign-up")]
    [SwaggerOperation(Summary = "Admin sign-up", OperationId = "SignUpAdmin")]
    public IActionResult SignUpAdmin([FromBody] SignUpAdminResource request)
    {
        var created = commandService.SignUpAdmin(request);
        var resource = UserAccountResourceFromEntityAssembler.ToResource(created);

        return Created(string.Empty, new
        {
            message = "Admin registered successfully",
            user = resource
        });
    }

    [HttpPost("admins/sign-in")]
    [SwaggerOperation(Summary = "Admin sign-in", OperationId = "SignInAdmin")]
    public IActionResult SignInAdmin([FromBody] SignInAdminResource request)
    {
        try
        {
            var user = commandService.SignInAdmin(request);
            var resource = UserAccountResourceFromEntityAssembler.ToResource(user);

            return Ok(new
            {
                message = "Admin login successful",
                user = resource
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("teachers")]
    [SwaggerOperation(Summary = "Create new teacher", OperationId = "CreateTeacher")]
    public IActionResult CreateTeacher([FromBody] CreateTeacherResource request)
    {
        var created = commandService.CreateTeacher(request);
        var resource = UserAccountResourceFromEntityAssembler.ToResource(created);

        return Created(string.Empty, new
        {
            message = "Teacher created successfully",
            user = resource
        });
    }

    [HttpPost("teachers/sign-in")]
    [SwaggerOperation(Summary = "Teacher sign-in", OperationId = "SignInTeacher")]
    public IActionResult SignInTeacher([FromBody] SignInTeacherResource request)
    {
        try
        {
            var user = commandService.SignInTeacher(request);
            var resource = UserAccountResourceFromEntityAssembler.ToResource(user);

            return Ok(new
            {
                message = "Teacher login successful",
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
    public IActionResult ResetPassword([FromBody] ResetPasswordResource request)
    {
        if (request.NewPassword != request.RepeatPassword)
            return BadRequest(new { message = "Passwords do not match" });

        commandService.ResetPassword(request);

        return Ok(new
        {
            message = "Password reset successfully",
            email = request.Email
        });
    }
}
