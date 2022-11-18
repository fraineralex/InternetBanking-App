using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class PaymentsViewModel
    {
        public string? Type { get; set; }
        public float? Amount { get; set; }

        public int OriginAccountId { get; set; }
        public int CreditCardId { get; set; }
        public int TargetAccountNumber { get; set; }
        public int CustomerId { get; set; }


    }
}
