using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Transform;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Billing.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/students/{dni}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Invoice Endpoints.")]
public class InvoiceController(
        IInvoiceCommandService invoiceCommandService,
        IInvoiceQueryService invoiceQueryService
    ) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation]
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The invoices were found",
        typeof(IEnumerable<InvoiceResource>)
    )]
    public async Task<IActionResult> GetAllInvoicesByDni([FromRoute] string dni)
    {
        var getAllInvoicesByDniQuery = new GetAllInvoicesByDniQuery(new Dni(dni));
        var invoices = await invoiceQueryService.Handle(getAllInvoicesByDniQuery);
        var invoiceResources = invoices
            .Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        return Ok(invoiceResources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromRoute] string dni, [FromBody] CreateInvoiceResource resource)
    {
        var createInvoiceCommand = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(dni, resource);
        var invoiceId = await invoiceCommandService.Handle(createInvoiceCommand);
        var getInvoiceByIdQuery = new GetInvoiceByIdQuery(invoiceId);
        var invoice = await invoiceQueryService.Handle(getInvoiceByIdQuery);
        if (invoice is null) return BadRequest("Invoice could not be created.");
        var invoiceResource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return CreatedAtAction(
            nameof(GetAllInvoicesByDni),
            new { dni = dni },
            invoiceResource
        );
    }
}