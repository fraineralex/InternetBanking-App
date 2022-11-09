
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Beneficiaries : AuditableBaseEntity
    {
        public string? Alias { get; set; }

        //Foreign Key
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }
        public SavingsAccounts? BeneficiaryAccount { get; set; }
    }
}
