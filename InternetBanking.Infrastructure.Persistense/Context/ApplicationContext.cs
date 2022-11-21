using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public DbSet<Beneficiaries> Beneficiaries { get; set; }
        public DbSet<CashAdvances> CashAdvances { get; set; }
        public DbSet<CreditCards> CreditCards { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<PersonalTransfers> PersonalTransfers { get; set; }
        public DbSet<SavingsAccounts> SavingsAccounts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreateBy = "InternetBankingApp";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        entry.Entity.LastModifiedBy = "InternetBankingApp";
                        break;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region Tables

            modelBuilder.Entity<SavingsAccounts>().ToTable("SavingAccounts");
            modelBuilder.Entity<CreditCards>().ToTable("CreditCards");
            modelBuilder.Entity<Loans>().ToTable("Loans");
            modelBuilder.Entity<Payments>().ToTable("Payments");
            modelBuilder.Entity<PersonalTransfers>().ToTable("PersonalTransfers");
            modelBuilder.Entity<CashAdvances>().ToTable("CashAdvances");
            modelBuilder.Entity<Beneficiaries>().ToTable("Beneficiaries");


            #endregion


            #region "Primary keys"
            modelBuilder.Entity<SavingsAccounts>().HasKey(savingAccount => savingAccount.Id);
            modelBuilder.Entity<CreditCards>().HasKey(creditCard => creditCard.Id);
            modelBuilder.Entity<Loans>().HasKey(loan => loan.Id);
            modelBuilder.Entity<Payments>().HasKey(payment => payment.Id);
            modelBuilder.Entity<PersonalTransfers>().HasKey(personalTransfer => personalTransfer.Id);
            modelBuilder.Entity<CashAdvances>().HasKey(cashAdvance => cashAdvance.Id);
            modelBuilder.Entity<Beneficiaries>().HasKey(beneficiary => beneficiary.Id);


            #endregion


            #region Relationships


            //RELATIONSHIP OF SAVINGSACCOUNTS WITH CREDIT CARDS
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.CreditCards)
                .WithOne(creditCard => creditCard.SavingsAccount)
                .HasForeignKey(creditCard => creditCard.UserMainAccountId)
                .OnDelete(DeleteBehavior.Cascade);


            //RELATIONSHIP OF SAVINGSACCOUNTS WITH LOANS
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.Loans)
                .WithOne(loan => loan.SavingsAccount)
                .HasForeignKey(loan => loan.TargetAccountNumber)
                .OnDelete(DeleteBehavior.Cascade);


            //RELATIONSHIP OF SAVINGSACCOUNTS WITH PAYMENTS
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.Payments)
                .WithOne(payment => payment.SavingsAccount)
                .HasForeignKey(payment => payment.OriginAccountId)
                .OnDelete(DeleteBehavior.Cascade);


            //RELATIONSHIP OF SAVINGSACCOUNTS PERSONAL TRANSFERS
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.PersonalTransfers)
                .WithOne(personalTransfer => personalTransfer.SavingsAccount)
                .HasForeignKey(personalTransfer => personalTransfer.OriginAccountId)
                .OnDelete(DeleteBehavior.Cascade);


            //RELATIONSHIP OF SAVINGSACCOUNTS WITH CASH ADVANCES
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.CashAdvances)
                .WithOne(cashAdvance => cashAdvance.SavingsAccount)
                .HasForeignKey(cashAdvance => cashAdvance.TargetAccountNumber)
                .OnDelete(DeleteBehavior.Cascade);

            //RELATIONSHIP OF SAVINGSACCOUNTS WITH BENEFICIARIES
            modelBuilder.Entity<SavingsAccounts>()
                .HasMany(savingsAccount => savingsAccount.Beneficiaries)
                .WithOne(beneficiary => beneficiary.BeneficiaryAccount)
                .HasForeignKey(beneficiary => beneficiary.AccountNumberId)
                .OnDelete(DeleteBehavior.Cascade);

            //RELATIONSHIP OF CREDIT CARDS WITH PAYMENTS
            modelBuilder.Entity<CreditCards>()
                .HasMany(creditCard => creditCard.Payments)
                .WithOne(payment => payment.CreditCard)
                .HasForeignKey(payment => payment.CreditCardId)
                .OnDelete(DeleteBehavior.NoAction);

            //RELATIONSHIP OF CREDIT CARDS WITH PERSONAL TRANSFER
            modelBuilder.Entity<CreditCards>()
                .HasMany(creditCard => creditCard.PersonalTransfers)
                .WithOne(personalTrasfer => personalTrasfer.CreditCard)
                .HasForeignKey(personalTrasfer => personalTrasfer.CreditCardId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region "Properties configurations"


            #region "Saving Accounts"

            modelBuilder.Entity<SavingsAccounts>()
                .HasIndex(savingAccount => savingAccount.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.AccountNumber)
                .IsRequired();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.TotalBalance)
                .IsRequired();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.IsMainAccount)
                .IsRequired();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.CustomerId)
                .IsRequired();

            #endregion

            #region "Credit Cards"
            modelBuilder.Entity<CreditCards>()
                .HasIndex(creditCard => creditCard.CreditCardNumber)
                .IsUnique();

            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.CreditCardNumber)
                .IsRequired();

            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.TotalBalance)
                .IsRequired();

            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.CreditAvailable)
                .IsRequired();

            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.ExpirationDate)
                .IsRequired();

            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.CardVerificationValue)
                .IsRequired();

            modelBuilder.Entity<CreditCards>()
               .Property(creditCard => creditCard.Password)
               .IsRequired();

            modelBuilder.Entity<CreditCards>()
               .Property(creditCard => creditCard.Company)
               .IsRequired();

            modelBuilder.Entity<CreditCards>()
               .Property(creditCard => creditCard.Status)
               .HasDefaultValue(true)
               .IsRequired();

            modelBuilder.Entity<CreditCards>()
               .Property(creditCard => creditCard.CustomerId)
               .IsRequired();

            #endregion

            #region Loans

            modelBuilder.Entity<Loans>()
                .Property(loan => loan.Type)
                .IsRequired();

            modelBuilder.Entity<Loans>()
                .Property(loan => loan.Amount)
                .IsRequired();

            modelBuilder.Entity<Loans>()
                .Property(loan => loan.TotalDebt)
                .IsRequired();

            modelBuilder.Entity<Loans>()
                .Property(loan => loan.AmountPaid)
                .IsRequired();

            modelBuilder.Entity<Loans>()
                .Property(loan => loan.Status)
                .HasDefaultValue(true)
                .IsRequired();

            #endregion

            #region Payments

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.Type);

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.Amount)
                .IsRequired();

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.OriginAccountId)
                .IsRequired();

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.CreditCardId);

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.TargetAccountNumber);

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.LoanId);

            modelBuilder.Entity<Payments>()
                .Property(payments => payments.BeneficiaryId);

            #endregion

            #region "Personal Transfer"

            modelBuilder.Entity<PersonalTransfers>()
                .Property(personalTransfer => personalTransfer.Amount)
                .IsRequired();

            modelBuilder.Entity<PersonalTransfers>()
                .Property(personalTransfer => personalTransfer.OriginAccountId)
                .IsRequired();

            modelBuilder.Entity<PersonalTransfers>()
                .Property(personalTransfer => personalTransfer.CreditCardId)
                .IsRequired();

            modelBuilder.Entity<PersonalTransfers>()
                .Property(personalTransfer => personalTransfer.TargetAccountNumber)
                .IsRequired();

            modelBuilder.Entity<PersonalTransfers>()
                .Property(personalTransfer => personalTransfer.CustomerId)
                .IsRequired();
            #endregion

            #region "Cash Advances"

            modelBuilder.Entity<CashAdvances>()
                .Property(cashAdvance => cashAdvance.Amount)
                .IsRequired();

            modelBuilder.Entity<CashAdvances>()
                .Property(cashAdvance => cashAdvance.OriginCreditCardId)
                .IsRequired();

            modelBuilder.Entity<CashAdvances>()
                .Property(cashAdvance => cashAdvance.TargetAccountNumber)
                .IsRequired();

            modelBuilder.Entity<CashAdvances>()
                .Property(cashAdvance => cashAdvance.CustomerId)
                .IsRequired();
            #endregion

            #region Beneficiaries

            modelBuilder.Entity<Beneficiaries>()
                .Property(beneficiary => beneficiary.Alias)
                .IsRequired();

            modelBuilder.Entity<Beneficiaries>()
                .Property(beneficiary => beneficiary.AccountNumberId)
                .IsRequired();

            modelBuilder.Entity<Beneficiaries>()
                .Property(beneficiary => beneficiary.CustomerId)
                .IsRequired();
            #endregion


            #endregion

        }


    }

}
