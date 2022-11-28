using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class TypeAccount : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
