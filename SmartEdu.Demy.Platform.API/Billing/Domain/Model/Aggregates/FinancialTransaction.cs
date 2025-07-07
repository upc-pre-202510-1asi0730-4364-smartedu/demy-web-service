using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;

public partial class FinancialTransaction
{
    public int Id { get; }

    public ETransactionType Type { get; private set; }

    public ETransactionCategory Category { get; private set; }

    public string Concept { get; private set; }

    public DateTime Date { get; private set; }

    public Payment Payment { get; private set; }

    private FinancialTransaction() { }

    public FinancialTransaction(string type, string category, string concept, DateTime date, Payment payment)
    {
        Type = Enum.Parse<ETransactionType>(type, true);
        Category = Enum.Parse<ETransactionCategory>(category, true);
        Concept = concept;
        Date = date;
        Payment = payment;
    }
}