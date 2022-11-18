using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class CashAdvancesSaveViewModel
    {


        [Required(ErrorMessage = "You must enter the Amount of the Cash Advance")]
        public float? Amount { get; set; }


        public int OriginCreditCardId { get; set; }
        public List<CreditCardsViewModel>?CreditCards { get; set; }
        public int TargetAccountNumber { get; set; }

        public int CustomerId { get; set; }


    }
}
