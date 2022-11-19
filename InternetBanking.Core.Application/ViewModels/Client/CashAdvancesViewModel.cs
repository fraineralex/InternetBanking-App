using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class CashAdvancesViewModel
    {
        public int Id { get; set; }
        public float? Amount { get; set; }

        //Foreign Key
        public int OriginCreditCardId { get; set; }
        public int TargetAccountNumber { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public SavingsAccountsViewModel? SavingsAccount { get; set; }
        public CreditCardsViewModel? CreditCard { get; set; }

    }
}
