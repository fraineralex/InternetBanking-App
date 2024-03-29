﻿using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserViewModel user = new();

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor http) : base(options) 
        {
            _httpContextAccessor = http;
        }

        #region dbSets -->
        public DbSet<Product> Products { get; set; }
        public DbSet<TypeAccount> TypeAccounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken ct = new())
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            }

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = user.UserName;

                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.ModifiedBy = user.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            #region tables
            mb.Entity<Product>()
                .ToTable("Products");

            mb.Entity<TypeAccount>()
                .ToTable("TypeAccounts");

            mb.Entity<Beneficiary>()
                .ToTable("Beneficiaries");

            mb.Entity<Payment>()
                .ToTable("Payments");
            #endregion

            #region primary keys

            mb.Entity<Product>()
                .HasKey(e => e.Id);

            mb.Entity<TypeAccount>()
                .HasKey(e => e.Id);

            mb.Entity<Beneficiary>()
                .HasKey(e => e.Id);

            mb.Entity<Payment>()
               .HasKey(e => e.Id);

            #endregion

            #region relations

            mb.Entity<TypeAccount>()
                .HasMany<Product>(t => t.Products)
                .WithOne(p => p.TypeAccount)
                .HasForeignKey(p => p.TypeAccountId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region property configurations

            #region Products
            mb.Entity<Product>()
                .Property(p => p.Discount)
                .HasDefaultValue(0.00)
                .IsRequired();
            #endregion

            #region Typeaccount

            mb.Entity<TypeAccount>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            #endregion

            #region Payment

            mb.Entity<Payment>()
                .Property(e => e.Amount)
                .IsRequired();
            
            mb.Entity<Payment>()
                .Property(e => e.OriginAccountNumber)
                .IsRequired();

            mb.Entity<Payment>()
                .Property(e => e.DestinationAccountNumber)
                .IsRequired();

            #endregion

            #endregion
        }
    }
}
