using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class SavingsAccounts : AuditableBaseEntity
    {
        public int AccountNumber { get; set; }
        public float? TotalBalance { get; set; }
        public bool? IsMainAccount { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public string? CustomerId { get; set; }

        //Navigation property
        public ICollection<CashAdvances>? CashAdvances { get; set; }
        public ICollection<PersonalTransfers>? PersonalTransfers { get; set; }
        public ICollection<Payments>? Payments { get; set; }
        public ICollection<Beneficiaries>? Beneficiaries { get; set; }
        public ICollection<Loans>? Loans { get; set; }
        public ICollection<CreditCards>? CreditCards { get; set; }

    }
}
