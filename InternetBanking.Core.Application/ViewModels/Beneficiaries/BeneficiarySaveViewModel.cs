using System.ComponentModel.DataAnnotations;


namespace InternetBanking.Core.Application.ViewModels.BeneficiarySaveViewModel
{
    public class BeneficiarySaveViewModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "You must indicate the account number")]
        public string? BeneficiaryAccountNumber { get; set; }
        public string? BeneficiaryName { get; set; }

    }
}
