﻿using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Products
{
    public class ProductSaveViewModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string? ClientId { get; set; }
        public string? AccountNumber { get; set; }
        public double Discount { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int TypeAccountId { get; set; }
        public DateTime? Created { get; set; }
        public TypeAccount? TypeAccount { get; set; }
    }
}
