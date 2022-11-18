using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class SavingsAccountsSaveViewModel
    {
        public int Id { get; set; }


        //public int AccountNumber { get; set; }


        [Required(ErrorMessage = "You must enter the Total Balance")]
        public float? TotalBalance { get; set; }


        //public bool? IsMainAccount { get; set; }


        [Required(ErrorMessage = "You must type the status")]
        public string? Status { get; set; }


        public int? CustomerId { get; set; }


        //public List<string>? Customers { get; set; }


    }
}
