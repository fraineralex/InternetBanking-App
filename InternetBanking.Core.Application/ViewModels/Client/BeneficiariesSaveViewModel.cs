using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class BeneficiariesSaveViewModel
    {

        [Required(ErrorMessage = "You must type the Alias of the Beneficiary")]
        public string? Alias { get; set; }


        public int AccountNumberId { get; set; }

        [Required(ErrorMessage = "You must type the number of the Beneficiary")]
        public string? AccountNumber { get; set; }


        public int CustomerId { get; set; }


    }
}
