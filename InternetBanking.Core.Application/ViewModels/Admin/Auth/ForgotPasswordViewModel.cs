using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Admin.Auth
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "You must type the email")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
