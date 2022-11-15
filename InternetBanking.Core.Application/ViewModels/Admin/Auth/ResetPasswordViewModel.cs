using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Admin.Auth
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "You must type the email")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "You must have a token")]
        [DataType(DataType.Text)]
        public string? Token { get; set; }

        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must be the same")]
        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
