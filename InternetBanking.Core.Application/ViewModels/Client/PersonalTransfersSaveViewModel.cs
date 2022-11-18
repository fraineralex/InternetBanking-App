using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class PersonalTransfersSaveViewModel
    {

        [Required(ErrorMessage = "You must enter the Amount of the transfer ")]
        public float? Amount { get; set; }


        public int OriginAccountId { get; set; }
        [Required(ErrorMessage = "You must enter the Account Number destination")]
        public int TargetAccountNumber { get; set; }


        public int CreditCardId { get; set; }
        public int CustomerId { get; set; }


    }
}
