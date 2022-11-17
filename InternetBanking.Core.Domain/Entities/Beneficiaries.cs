
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Beneficiaries : AuditableBaseEntity
    {
        public string? Alias { get; set; }

        //Foreign Key
        public int AccountNumberId { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public SavingsAccounts? BeneficiaryAccount { get; set; }
    }
}
