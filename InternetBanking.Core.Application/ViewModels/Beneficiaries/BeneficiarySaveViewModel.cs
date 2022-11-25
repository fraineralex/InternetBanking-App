using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.BeneficiarySaveViewModel
{
    public class BeneficiarySaveViewModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Tiene que indicar un numero de cuenta")]
        public string? BeneficiaryAccountNumber { get; set; }
        public string? BeneficiaryName { get; set; }

    }
}
