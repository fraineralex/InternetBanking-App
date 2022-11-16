using InternetBanking.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? UserType { get; set; }
        public string? ProfileImage { get; set; }
        public string? IDCard { get; set; }

        // Initial Amount if the user type is a CLIENT  
        public double? InitialAmount { get; set; }


    }
}
