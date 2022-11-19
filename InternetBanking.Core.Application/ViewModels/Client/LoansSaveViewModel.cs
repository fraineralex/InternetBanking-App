﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class LoansSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must select the type of the loan")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "You must choose the amount of the loan")]
        public float? Amount { get; set; }

        public float? TotalDebt { get; set; }
        public float? AmountPaid { get; set; }
        
        [Required(ErrorMessage = "You must specify the status of the loan")]
        public string? Status { get; set; }
        public int TargetAccountNumber { get; set; }
        public int CustomerId { get; set; }

    }
}