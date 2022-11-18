using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class CreditCardsSaveViewModel
    {
        
        //public int CreditCardNumber { get; set; }


        [Required(ErrorMessage = "You must enter the Total Balance")]
        public float TotalBalance { get; set; }

        //public float? CreditAvailable { get; set; }

        //public DateTime ExpirationDate { get; set; }

        //public int CardVerificationValue { get; set; }
        
        [Required(ErrorMessage = "You must type the Password of the Credit Card")]
        public int Password { get; set; }

        [Required(ErrorMessage = "You must type the Company of the Credit Card")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "You must type the Status of the Credit Card")]
        public string? Status { get; set; }



        //public int UserMainAccountId { get; set; }


        [Required(ErrorMessage = "You must select the User who you are creating the Credit Card")]
        public int CustomerId { get; set; }


    }
}
