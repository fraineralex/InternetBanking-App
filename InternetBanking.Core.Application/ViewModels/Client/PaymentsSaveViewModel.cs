using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class PaymentsSaveViewModel
    {
        public string? Type { get; set; }

        [Required(ErrorMessage = "You must enter the Amount that you want to pay")]
        public float? Amount { get; set; }



        [Required(ErrorMessage = "You must type the Account that you want to pay")]
        [StringLength(9, ErrorMessage = "The Account only has 9 digits")]
        public int TargetAccountNumber { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "You must select the Account from where you'll pay ")]
        public int OriginAccountId { get; set; }
        public List<SavingsAccountsViewModel>? UserAccountsNumbers { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "You must select the Credit Card that you want to pay ")]
        public int CreditCardId { get; set; }

        public List<CreditCardsViewModel>? UserCreditCards { get; set; }

        public string? CustomerId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "You must select the Loan to which you'll pay")]
        public int LoanId { get; set; }
        public List<LoansViewModel>? UserLoans { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "You must select the Beneficiary to which you'll pay")]
        public int BeneficiaryId { get; set; }
        public List<BeneficiariesViewModel>? UserBeneficiaries { get; set; }


    }
}
