using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Loans : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public float? Amount { get; set; }
        public float? TotalDebt { get; set; }
        public float? AmountPaid { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int TargetAccountNumber { get; set; }
        public string? CustomerId { get; set; }

        //Navigation property
        public SavingsAccounts? SavingsAccount { get; set; }

    }
}
