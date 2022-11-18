using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class CashAdvancesViewModel
    {
        public float? Amount { get; set; }

        public string? OriginCreditCard { get; set; }
        public string? TargetAccountNumber { get; set; }



    }
}
