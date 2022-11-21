using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Application.ViewModels.Client;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region "User Profile"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.UserType, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();


            #endregion

            #region "Loans profile"
            CreateMap<Loans, LoansViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<Loans, LoansSaveViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.SavingsAccount, opt => opt.Ignore());

            #endregion

            #region "Beneficiaries profile"
            CreateMap<Beneficiaries, BeneficiariesViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<Beneficiaries, BeneficiariesSaveViewModel>()
                .ForMember(x => x.AccountNumber, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.BeneficiaryAccount, opt => opt.Ignore());

            #endregion

            #region "CashAdvances profile"
            CreateMap<CashAdvances, CashAdvancesViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<CashAdvances, CashAdvancesSaveViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.CreditCard, opt => opt.Ignore())
                .ForMember(x => x.SavingsAccount, opt => opt.Ignore());

            #endregion

            #region "CreditCards  profile"
            CreateMap<CreditCards, CreditCardsViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<CreditCards, CreditCardsSaveViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.SavingsAccount, opt => opt.Ignore())
                .ForMember(x => x.PersonalTransfers, opt => opt.Ignore())
                .ForMember(x => x.Payments, opt => opt.Ignore());

            #endregion

            #region "Payments profile"
            CreateMap<Payments, PaymentsViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<Payments, PaymentsSaveViewModel>()
                .ForMember(x => x.UserAccountsNumbers, opt => opt.Ignore())
                .ForMember(x => x.UserCreditCards, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.CreditCard, opt => opt.Ignore())
                .ForMember(x => x.SavingsAccount, opt => opt.Ignore());

            #endregion

            #region "PersonalTransfers profile"
            CreateMap<PersonalTransfers, PersonalTransfersViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<PersonalTransfers, PersonalTransfersSaveViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.CreditCard, opt => opt.Ignore())
                .ForMember(x => x.SavingsAccount, opt => opt.Ignore());

            #endregion

            #region "SavingsAccounts profile"
            CreateMap<SavingsAccounts, SavingsAccountsViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore());

            CreateMap<SavingsAccounts, SavingsAccountsSaveViewModel> ()
                .ReverseMap()
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedAt, opt => opt.Ignore())
                .ForMember(x => x.CashAdvances, opt => opt.Ignore())
                .ForMember(x => x.Payments, opt => opt.Ignore())
                .ForMember(x => x.Beneficiaries, opt => opt.Ignore())
                .ForMember(x => x.Loans, opt => opt.Ignore())
                .ForMember(x => x.CreditCards, opt => opt.Ignore())
                .ForMember(x => x.PersonalTransfers, opt => opt.Ignore());

            #endregion
        }
    }
}
