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
[SwaggerTag("Available Financial Transactions Endpoints.")]
public class FinancialTransactionsController(
    IFinancialTransactionCommandService financialTransactionCommandService,
    IFinancialTransactionQueryService financialTransactionQueryService
    ) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFinancialTransactionById([FromRoute] int financialTransactionId)
    {
        var getFinancialTransactionByIdQuery = new GetFinancialTransactionByIdQuery(financialTransactionId);
        var financialTransaction = await financialTransactionQueryService.Handle(getFinancialTransactionByIdQuery);
        if (financialTransaction is null) return NotFound();
        var financialTransactionResource = FinancialTransactionResourceFromEntityAssembler.ToResourceFromEntity(financialTransaction);
        return Ok(financialTransactionResource);
    }

    [HttpGet]
    [SwaggerOperation]
    public async Task<IActionResult> GetAllFinancialTransactions()
    {
        var getAllFinancialTransactionsQuery = new GetAllFinancialTransactionsQuery();
        var financialTransactions = await financialTransactionQueryService.Handle(getAllFinancialTransactionsQuery);
        var financialTransactionResources =
            financialTransactions.Select(FinancialTransactionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(financialTransactionResources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFinancialTransaction([FromBody] CreateFinancialTransactionResource resource)
    {
        var createFinancialTransactionCommand = CreateFinancialTransactionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var financialTransaction = await financialTransactionCommandService.Handle(createFinancialTransactionCommand);
        if (financialTransaction is null) return BadRequest("Financial transaction could not be created.");
        var financialTransactionResource = FinancialTransactionResourceFromEntityAssembler.ToResourceFromEntity(financialTransaction);
        return CreatedAtAction(
            nameof(GetFinancialTransactionById),
            new { id = financialTransaction.Id },
            financialTransactionResource
        );
    }

    [HttpPost("/invoices/{invoiceId}/payment")]
    public async Task<IActionResult> RegisterPayment([FromRoute] int invoiceId,
        [FromBody] RegisterPaymentResource resource)
    {
        var registerPaymentCommand = RegisterPaymentCommandFromResourceAssembler.ToCommandFromResource(invoiceId, resource);
        var financialTransaction = await financialTransactionCommandService.Handle(registerPaymentCommand);
        if (financialTransaction is null) return BadRequest("Payment could not be created.");
        var financialTransactionResource = FinancialTransactionResourceFromEntityAssembler.ToResourceFromEntity(financialTransaction);
        return CreatedAtAction(
            nameof(GetFinancialTransactionById),
            new { id = financialTransaction.Id },
            financialTransactionResource
        );
    }

    [HttpPost("/expenses")]
    public async Task<IActionResult> RegisterExpense([FromBody] RegisterExpenseResource resource)
    {
        var registerExpenseCommand = RegisterExpenseCommandFromResourceAssembler.ToCommandFromResource(resource);
        var financialTransaction = await financialTransactionCommandService.Handle(registerExpenseCommand);
        if (financialTransaction is null) return BadRequest("Expense could not be created.");
        var financialTransactionResource = FinancialTransactionResourceFromEntityAssembler.ToResourceFromEntity(financialTransaction);
        return CreatedAtAction(
            nameof(GetFinancialTransactionById),
            new { id = financialTransaction.Id },
            financialTransactionResource
        );
    }
}