using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class CreditCards : AuditableBaseEntity
    {
        public string? CreditCardType { get; set; }
        public float TotalBalance { get; set; }
        public float? CreditAvailable { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CardVerificationValue { get; set; }
        public int Password { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int CustomerId { get; set; }

        //Navigation property
        public Users? Customer { get; set; }

    }
}
