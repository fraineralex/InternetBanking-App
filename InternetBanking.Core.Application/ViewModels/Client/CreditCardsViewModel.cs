using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class CreditCardsViewModel
    {
        public int Id { get; set; }
        public int CreditCardNumber { get; set; }
        public float TotalBalance { get; set; }
        public float? CreditAvailable { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CardVerificationValue { get; set; }
        //public int Password { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int UserMainAccountId { get; set; }
        public string? CustomerId { get; set; }

        //Navigation property
        public SavingsAccountsViewModel? SavingsAccount { get; set; }
        public ICollection<PaymentsViewModel>? Payments { get; set; }
        public ICollection<PersonalTransfersViewModel>? PersonalTransfers { get; set; }
    }
}
