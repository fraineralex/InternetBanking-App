
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
        public string? IDCard { get; set; }
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
