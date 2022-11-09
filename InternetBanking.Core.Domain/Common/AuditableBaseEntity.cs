
namespace InternetBanking.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }

        public virtual string? CreateBy { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual string? LastModifiedBy { get; set; }

        public virtual DateTime? LastModifiedAt { get; set; }
    }
}
