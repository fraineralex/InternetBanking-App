using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public double Amount { get; set; }
        public string? ClientId { get; set; }
        public string? AccountNumber { get; set; }
        public double? Discount { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int TypeAccountId { get; set; }
        public TypeAccount? TypeAccount { get; set; }
    }
}
