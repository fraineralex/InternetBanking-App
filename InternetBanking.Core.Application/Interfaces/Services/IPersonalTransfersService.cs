using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPersonalTransfersService : IGenericService<SavePersonalTransfersViewModel, PersonalTransfersViewModel, PersonalTransfers>
    {
        Task<List<PersonalTransfersViewModel>> GetAllViewModelWithInclude();
        Task<PersonalTransfersViewModel> GetPostViewModelById(int id);
        Task<List<PersonalTransfersViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
