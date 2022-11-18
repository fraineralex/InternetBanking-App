using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class LoansViewModel
    {
        public int CreditCardNumber { get; set; }
        public float TotalBalance { get; set; }
        public float? CreditAvailable { get; set; }
        public string? ExpirationDate { get; set; }

        //public int CardVerificationValue { get; set; }
        //public int Password { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }

        //public int UserMainAccountId { get; set; }
        //public int CustomerId { get; set; }



    }
}
