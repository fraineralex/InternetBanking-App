using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class PaymentViewModel
    {
        public double Amount { get; set; }
        public string? OriginAccountNumber { get; set; }
        public string? DestinationAccountNumber { get; set; }
    }
}
