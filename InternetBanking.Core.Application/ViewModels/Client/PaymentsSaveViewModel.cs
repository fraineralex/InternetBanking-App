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
        //public string? Type { get; set; }

        [Required(ErrorMessage = "You must enter the Amount that you want to pay")]
        public float? Amount { get; set; }


        public int OriginAccountId { get; set; }

        [Required(ErrorMessage = "You must select the Account that you want to pay")]
        public int TargetAccountNumber { get; set; }


        public int CreditCardId { get; set; }
        public int CustomerId { get; set; }


    }
}
