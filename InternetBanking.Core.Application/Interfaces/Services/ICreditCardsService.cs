using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICreditCardsService : IGenericService<SaveCreditCardsViewModel, CreditCardsViewModel, CreditCards>
    {
        Task<List<CreditCardsViewModel>> GetAllViewModelWithInclude();
        Task<CreditCardsViewModel> GetPostViewModelById(int id);
        Task<List<CreditCardsViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
