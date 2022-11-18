using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ISavingsAccountsService : IGenericService<SaveSavingsAccountsViewModel, SavingsAccountsViewModel, SavingsAccounts>
    {
        Task<List<SavingsAccountsViewModel>> GetAllViewModelWithInclude();
        Task<SavingsAccountsViewModel> GetPostViewModelById(int id);
        Task<List<SavingsAccountsViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
