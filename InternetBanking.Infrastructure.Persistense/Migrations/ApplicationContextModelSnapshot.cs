﻿// <auto-generated />
using System;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiaries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumberId")
                        .HasColumnType("int");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumberId");

                    b.ToTable("Beneficiaries", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CashAdvances", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Amount")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreditCardId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OriginCreditCardId")
                        .HasColumnType("int");

                    b.Property<int>("TargetAccountNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("TargetAccountNumber");

                    b.ToTable("CashAdvances", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CreditCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardVerificationValue")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<float?>("CreditAvailable")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<int>("CreditCardNumber")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Password")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalBalance")
                        .HasColumnType("real");

                    b.Property<int>("UserMainAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardNumber")
                        .IsUnique();

                    b.HasIndex("UserMainAccountId");

                    b.ToTable("CreditCards", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Loans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Amount")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<float?>("AmountPaid")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TargetAccountNumber")
                        .HasColumnType("int");

                    b.Property<float?>("TotalDebt")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TargetAccountNumber");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Payments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Amount")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreditCardId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OriginAccountId")
                        .HasColumnType("int");

                    b.Property<int>("TargetAccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("OriginAccountId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.PersonalTransfers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Amount")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreditCardId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OriginAccountId")
                        .HasColumnType("int");

                    b.Property<int>("TargetAccountNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("OriginAccountId");

                    b.ToTable("PersonalTransfers", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.SavingsAccounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsMainAccount")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("TotalBalance")
                        .IsRequired()
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.ToTable("SavingAccounts", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiaries", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "BeneficiaryAccount")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("AccountNumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BeneficiaryAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CashAdvances", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.CreditCards", "CreditCard")
                        .WithMany()
                        .HasForeignKey("CreditCardId");

                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "SavingsAccount")
                        .WithMany("CashAdvances")
                        .HasForeignKey("TargetAccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CreditCards", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "SavingsAccount")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserMainAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Loans", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "SavingsAccount")
                        .WithMany("Loans")
                        .HasForeignKey("TargetAccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Payments", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.CreditCards", "CreditCard")
                        .WithMany("Payments")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "SavingsAccount")
                        .WithMany("Payments")
                        .HasForeignKey("OriginAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.PersonalTransfers", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.CreditCards", "CreditCard")
                        .WithMany("PersonalTransfers")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.SavingsAccounts", "SavingsAccount")
                        .WithMany("PersonalTransfers")
                        .HasForeignKey("OriginAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CreditCards", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("PersonalTransfers");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.SavingsAccounts", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("CashAdvances");

                    b.Navigation("CreditCards");

                    b.Navigation("Loans");

                    b.Navigation("Payments");

                    b.Navigation("PersonalTransfers");
                });
#pragma warning restore 612, 618
        }
    }
}