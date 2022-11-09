
using InternetBanking.Core.Domain.Common;
using System.Xml.Linq;

namespace InternetBanking.Core.Domain.Entities
{
    public class CashAdvances : AuditableBaseEntity
    {
        public float? Amount { get; set; }

        //Foreign Key
        public int OriginCreditCard { get; set; }
        public int TargetAccount { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }
    }
}
