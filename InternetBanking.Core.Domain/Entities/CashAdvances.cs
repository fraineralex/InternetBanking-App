
using InternetBanking.Core.Domain.Common;
using System.Xml.Linq;

namespace InternetBanking.Core.Domain.Entities
{
    public class CashAdvances : AuditableBaseEntity
    {
        public float? Amount { get; set; }

        //Foreign Key
        public int OriginCreditCardId { get; set; }
        public int TargetAccountNumber { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public SavingsAccounts? SavingsAccount { get; set; }
        public CreditCards? CreditCard { get; set; }
    }
}
