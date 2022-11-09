
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Payments : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public float? Amount { get; set; }

        //Foreign Key
        public int OriginAccount { get; set; }
        public int TargetAccount { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }
    }
}
