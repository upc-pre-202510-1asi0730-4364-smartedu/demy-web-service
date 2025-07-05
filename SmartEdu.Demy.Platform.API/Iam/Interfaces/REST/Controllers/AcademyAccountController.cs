using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Iam.Domain.Services;
using SmartEdu.Demy.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Iam.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Iam.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/academies")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Academy administration endpoints")]
public class AcademiesController : ControllerBase
{
    private readonly IAcademyCommandService _commandService;

    public AcademiesController(IAcademyCommandService commandService)
    {
        _commandService = commandService;
    }
    
    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation(Summary = "Create new academy", OperationId = "CreateAcademy")]
    public async Task<IActionResult> Create([FromBody] CreateAcademyResource request)
    {
        try
        {
            var command = new CreateAcademyCommand(request.UserId, request.AcademyName, request.Ruc);
            var academy = await _commandService.Handle(command);

            var resource = AcademyResourceFromEntityAssembler.ToResource(academy);

            return Created(string.Empty, new
            {
                message = "Academy created successfully",
                academy = resource
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}