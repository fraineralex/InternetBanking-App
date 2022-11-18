using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICashAdvancesService : IGenericService<SaveCashAdvancesViewModel, CashAdvancesViewModel, CashAdvances>
    {
        Task<List<CashAdvancesViewModel>> GetAllViewModelWithInclude();
        Task<CashAdvancesViewModel> GetPostViewModelById(int id);
        Task<List<CashAdvancesViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
