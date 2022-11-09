using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Loans : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public float? Amount { get; set; }
        public float? TotalDebt { get; set; }
        public float? AmountPaid { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MonthAmount { get; set; }
        public float? MonthFee { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }

    }
}
