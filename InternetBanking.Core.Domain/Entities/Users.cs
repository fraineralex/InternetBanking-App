using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Users : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Email { get; set; }
        public string? IDCard { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }


        //Navigation property
        public ICollection<CashAdvances>? CashAdvances { get; set; }
        public ICollection<PersonalTransfers>? PersonalTransfers { get; set; }
        public ICollection<Payments>? Payments { get; set; }
        public ICollection<Beneficiaries>? Beneficiaries { get; set; }
        public ICollection<Loans>? Loans { get; set; }
        public ICollection<CreditCards>? CreditCards { get; set; }
        public ICollection<SavingsAccounts>? SavingsAccounts { get; set; }


    }
}
