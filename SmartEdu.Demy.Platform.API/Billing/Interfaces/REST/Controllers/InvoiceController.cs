using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Invoice Endpoints.")]
public class InvoiceController(
        IInvoiceQueryService invoiceQueryService
    ) : ControllerBase
{
    [HttpGet("{studentId:long}")]
    [SwaggerOperation(
        Summary = "Get All Student ID",
        Description = "Get all invoices by student ID.",
        OperationId = "GetAllInvoicesByStudentId"
    )]
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The invoices were found",
        typeof(IEnumerable<InvoiceResource>)
    )]
    public async Task<IActionResult> GetAllInvoicesByStudentId(long studentId)
    {
        var getAllInvoicesByStudentIdQuery = new GetAllInvoicesByStudentIdQuery(studentId);
        var invoices = await invoiceQueryService.Handle(getAllInvoicesByStudentIdQuery);
        
        var invoiceResources = invoices
            .Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        
        return Ok(invoiceResources);
    }
}