using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Beneficiaries> beneficiaries { get; set; }
        public DbSet<CashAdvances>  cashAdvances{ get; set; }
        public DbSet<CreditCards> creditCards { get; set; }
        public DbSet<Loans> loans { get; set; }
        public DbSet<Payments> payments { get; set; }
        public DbSet<PersonalTransfers> personalTransfers { get; set; }
        public DbSet<SavingsAccounts> savingsAccounts { get; set; }
    }
}
