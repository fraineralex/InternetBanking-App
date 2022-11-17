
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Payments : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public float? Amount { get; set; }

        //Foreign Key
        public int OriginAccountId { get; set; }
        public int CreditCardId { get; set; }
        public int TargetAccountNumber { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public SavingsAccounts? SavingsAccount { get; set; }
        public CreditCards? CreditCard { get; set; }
    }
}
