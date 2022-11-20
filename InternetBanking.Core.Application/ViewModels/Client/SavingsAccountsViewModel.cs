using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class SavingsAccountsViewModel
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public float? TotalBalance { get; set; }
        public bool? IsMainAccount { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public string? CustomerId { get; set; }

        //Navigation property
        public ICollection<CashAdvancesViewModel>? CashAdvances { get; set; }
        public ICollection<PersonalTransfersViewModel>? PersonalTransfers { get; set; }
        public ICollection<PaymentsViewModel>? Payments { get; set; }
        public ICollection<BeneficiariesViewModel>? Beneficiaries { get; set; }
        public ICollection<LoansViewModel>? Loans { get; set; }
        public ICollection<CreditCardsViewModel>? CreditCards { get; set; }

    }
}
