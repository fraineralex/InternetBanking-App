using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class SavingsAccounts : AuditableBaseEntity
    {
        public float? TotalBalance { get; set; }
        public bool? IsMainAccount { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }

    }
}
