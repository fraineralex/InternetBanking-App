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

        public DbSet<Users> users { get; set; }
        public DbSet<Beneficiaries> beneficiaries { get; set; }
        public DbSet<CashAdvances>  cashAdvances{ get; set; }
        public DbSet<CreditCards> creditCards { get; set; }
        public DbSet<Loans> loans { get; set; }
        public DbSet<Payments> payments { get; set; }
        public DbSet<PersonalTransfers> personalTransfers { get; set; }
        public DbSet<SavingsAccounts> savingsAccounts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        break;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<SavingsAccounts>().ToTable("SavingAccounts");
            modelBuilder.Entity<CreditCards>().ToTable("CreditCards");
            modelBuilder.Entity<Loans>().ToTable("Loans");
            modelBuilder.Entity<Payments>().ToTable("Payments");
            modelBuilder.Entity<PersonalTransfers>().ToTable("PersonalTransfers");
            modelBuilder.Entity<CashAdvances>().ToTable("CashAdvances");

            
            #endregion


            #region "primary keys"
            modelBuilder.Entity<Users>().HasKey(user => user.Id);
            modelBuilder.Entity<SavingsAccounts>().HasKey(savingAccount => savingAccount.Id);
            modelBuilder.Entity<CreditCards>().HasKey(creditCard => creditCard.Id);
            modelBuilder.Entity<Loans>().HasKey(loan => loan.Id);
            modelBuilder.Entity<Payments>().HasKey(payment => payment.Id);
            modelBuilder.Entity<PersonalTransfers>().HasKey(personalTransfer => personalTransfer.Id);
            modelBuilder.Entity<CashAdvances>().HasKey(cashAdvance => cashAdvance.Id);


            #endregion


            #region relationship

            //RELATION USER WITH SAVING ACCOUNTS
            modelBuilder.Entity<Users>()
                .HasMany(user => user.SavingsAccounts)
                .WithOne(savingAccount => savingAccount.Customer)
                .HasForeignKey(savingAccount => savingAccount.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SavingsAccounts>()  
                .HasOne(savingAccount => savingAccount.Customer)
                .WithMany(user => user.SavingsAccounts)
                .HasForeignKey(savingAccount => savingAccount.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);



            //RELATION USER WITH CREDIT CARDS
            modelBuilder.Entity<Users>()
                .HasMany(user => user.CreditCards)
                .WithOne(creditCard => creditCard.Customer)
                .HasForeignKey(creditCard => creditCard.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CreditCards>()
                .HasOne(creditCard => creditCard.Customer)
                .WithMany(user => user.CreditCards)
                .HasForeignKey(creditCard => creditCard.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);




            //RELATION USER WITH LOANS
            modelBuilder.Entity<Users>()
                .HasMany(user => user.Loans)
                .WithOne(loan => loan.Customer)
                .HasForeignKey(loan => loan.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Loans>()
                .HasOne(loan => loan.Customer)
                .WithMany(user => user.Loans)
                .HasForeignKey(loan => loan.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);




            //RELATION USER WITH PAYMENTS
            modelBuilder.Entity<Users>()
                .HasMany(user => user.Payments)
                .WithOne(payment => payment.Customer)
                .HasForeignKey(payment => payment.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payments>()
                .HasOne(payment => payment.Customer)
                .WithMany(user => user.Payments)
                .HasForeignKey(payment => payment.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);


            //RELATION USER WITH PERSONAL TRANSFERS
            modelBuilder.Entity<Users>()
                .HasMany(user => user.PersonalTransfers)
                .WithOne(personalTransfer => personalTransfer.Customer)
                .HasForeignKey(personalTransfer => personalTransfer.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonalTransfers>()
                .HasOne(personalTransfers => personalTransfers.Customer)
                .WithMany(user => user.PersonalTransfers)
                .HasForeignKey(personalTransfers => personalTransfers.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);



            //RELATION USER WITH CASH ADVANCES
            modelBuilder.Entity<Users>()
                .HasMany(user => user.CashAdvances)
                .WithOne(cashAdvance => cashAdvance.Customer)
                .HasForeignKey(cashAdvance => cashAdvance.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CashAdvances>()
                .HasOne(cashAdvances => cashAdvances.Customer)
                .WithMany(user => user.CashAdvances)
                .HasForeignKey(cashAdvance => cashAdvance.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion


            #region "property configurations"


            #region savingAccounts

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.TotalBalance)
                .IsRequired();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.IsMainAccount)
                .IsRequired();

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.Status);

            modelBuilder.Entity<SavingsAccounts>()
                .Property(savingAccount => savingAccount.CustomerId)
                .IsRequired();

            #endregion

            #region creditCards
            modelBuilder.Entity<CreditCards>()
                .Property(creditCard => creditCard.CreditCardType)
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
               .IsRequired();

            modelBuilder.Entity<CreditCards>()
               .Property(creditCard => creditCard.CustomerId)
               .IsRequired();

            #endregion




            #endregion

        }


    }

}
