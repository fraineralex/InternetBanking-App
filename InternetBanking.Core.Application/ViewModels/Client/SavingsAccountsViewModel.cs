using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class SavingsAccountsViewModel
    {
        public int AccountNumber { get; set; }
        public float? TotalBalance { get; set; }
        public bool? IsMainAccount { get; set; }
        public string? Status { get; set; }
        
        public int CustomerId { get; set; }

    }
}
