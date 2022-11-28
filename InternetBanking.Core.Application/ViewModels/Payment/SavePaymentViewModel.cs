using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class SavePaymentViewModel
    {
        public double Amount { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string? OriginAccountNumber { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string? DestinationAccountNumber { get; set; }

        public string? Owner { get; set; }
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
        public int? TypeOfPayment { get; set; }
    }
}
