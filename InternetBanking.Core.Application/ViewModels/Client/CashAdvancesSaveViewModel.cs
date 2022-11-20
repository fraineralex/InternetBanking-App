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
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the Amount of the Cash Advance")]
        public float? Amount { get; set; }

        [Required(ErrorMessage = "You must enter the Origin Credit Card of the Cash Advance")]
        public int OriginCreditCardId { get; set; }
        [Required(ErrorMessage = "You must enter the Target Account Number of the Cash Advance")]
        public int TargetAccountNumber { get; set; }
        public string? CustomerId { get; set; }

    }
}
