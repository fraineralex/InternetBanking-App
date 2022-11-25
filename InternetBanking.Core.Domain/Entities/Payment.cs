using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Domain.Entities
{
    public class Payment : AuditableBaseEntity
    {
        public double Amount { get; set; }
        public string? OriginAccountNumber { get; set; }
        public string? DestinationAccountNumber { get; set; }

        // Foreign Key
        public int? TypeOfPayment { get; set; }
    }
}
