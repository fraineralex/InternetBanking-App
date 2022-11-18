using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ILoansService : IGenericService<SaveLoansViewModel, LoansViewModel, Loans>
    {
        Task<List<LoansViewModel>> GetAllViewModelWithInclude();
        Task<LoansViewModel> GetPostViewModelById(int id);
        Task<List<LoansViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
