using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class BeneficiariesViewModel
    {
        public int Id { get; set; }
        public string? Alias { get; set; }

        //Foreign Key
        public int AccountNumberId { get; set; }
        public int CustomerId { get; set; }

        //Navigation property
        public SavingsAccountsViewModel? BeneficiaryAccount { get; set; }

    }
}
