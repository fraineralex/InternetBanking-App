using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class CreditCards : AuditableBaseEntity
    {
        public int CreditCardNumber { get; set; }
        public float TotalBalance { get; set; }
        public float? CreditAvailable { get; set; }
        public string? ExpirationDate { get; set; }
        public int CardVerificationValue { get; set; }
        public int Password { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int UserMainAccountId { get; set; }
        public string? CustomerId { get; set; }

        //Navigation property
        public SavingsAccounts? SavingsAccount { get; set; }
        public ICollection<Payments>? Payments { get; set; }
        public ICollection<PersonalTransfers>? PersonalTransfers { get; set; }

    }
}
