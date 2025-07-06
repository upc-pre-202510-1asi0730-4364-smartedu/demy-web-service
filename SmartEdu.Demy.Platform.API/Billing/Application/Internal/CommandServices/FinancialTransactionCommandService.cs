using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Billing.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Billing.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Billing.Application.Internal.CommandServices;

public class FinancialTransactionCommandService(
    IFinancialTransactionRepository financialTransactionRepository, IInvoiceRepository invoiceRepository,
    IUnitOfWork unitOfWork) : IFinancialTransactionCommandService
{
    public async Task<FinancialTransaction?> Handle(CreateFinancialTransactionCommand command)
    {
        var payment = new Payment(
            command.amount,
            command.currency,
            command.method,
            command.paidAt,
            command.invoiceId
        );

        var financialTransaction = new FinancialTransaction(
            command.type,
            command.category,
            command.concept,
            command.date,
            payment
        );

        try
        {
            await financialTransactionRepository.AddAsync(financialTransaction);
            await unitOfWork.CompleteAsync();
            return financialTransaction;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while adding new financial transaction: {ex.Message}");
        }
    }

    // Transactional
    public async Task<FinancialTransaction?> Handle(RegisterPaymentCommand command)
    {
        var invoice = await invoiceRepository.FindByIdAsync(command.invoiceId);
        if (invoice is null) throw new Exception($"Invoice not found with id {command.invoiceId}");
        if (invoice.IsPaid()) throw new Exception("Invoice already paid");

        var payment = new Payment(
            invoice.Amount,
            invoice.Currency,
            command.method,
            DateTime.Now,
            command.invoiceId
        );

        var financialTransaction = new FinancialTransaction(
            "Income",
            "Students",
            $"Paid student invoice {invoice.Name} with DNI {invoice.Dni}",
            DateTime.Now,
            payment
        );

        try
        {
            invoice.MarkAsPaid();
            await invoiceRepository.AddAsync(invoice);
            await financialTransactionRepository.AddAsync(financialTransaction);
            await unitOfWork.CompleteAsync();
            return financialTransaction;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while adding new financial transaction: {ex.Message}");
        }
    }

    public async Task<FinancialTransaction?> Handle(RegisterExpenseCommand command)
    {
        var payment = new Payment(
            command.amount,
            command.currency,
            command.method,
            command.paidAt
        );

        var financialTransaction = new FinancialTransaction(
            type: "Expense",
            command.category,
            command.concept,
            DateTime.Now,
            payment
        );

        try
        {
            await financialTransactionRepository.AddAsync(financialTransaction);
            return financialTransaction;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while adding new financial transaction: {ex.Message}");
        }
    }
}