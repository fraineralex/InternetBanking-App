using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.BeneficiaryViewModel
{
    public class BeneficiaryViewModel
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }
        public UserViewModel? User { get; set; }

        public string? BeneficiaryAccountNumber { get; set; }
        public  ProductViewModel? Beneficiary { get; set; }
        public string? BeneficiaryName { get; set; }

    }
}
