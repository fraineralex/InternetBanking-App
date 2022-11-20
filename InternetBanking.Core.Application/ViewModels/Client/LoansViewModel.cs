using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class LoansViewModel
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public float? Amount { get; set; }
        public float? TotalDebt { get; set; }
        public float? AmountPaid { get; set; }
        public string? Status { get; set; }

        //Foreign Key
        public int TargetAccountNumber { get; set; }
        public string? CustomerId { get; set; }

        //Navigation property
        public SavingsAccountsViewModel? SavingsAccount { get; set; }



    }
}
