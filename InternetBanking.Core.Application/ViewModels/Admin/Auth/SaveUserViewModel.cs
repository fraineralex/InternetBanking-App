using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Admin.Auth
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "You must type your First Name")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "You must type your Last Name")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "You must type the User's ID")]
        [DataType(DataType.Text)]
        public string? UserId { get; set; }


        [Required(ErrorMessage = "You must type the Username")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must be the same")]
        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "You must type the email")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "You must select the User's Type")]
        public List<string>? UserType { get; set; }


        // Initial Amount if the user type is a CLIENT  
        [DataType(DataType.Currency)]
        public double? InitialAmount { get; set; }

        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
