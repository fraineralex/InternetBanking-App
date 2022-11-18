using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiariesService : IGenericService<SaveBeneficiariesViewModel, BeneficiariesViewModel, Beneficiaries>
    {
        Task<List<BeneficiariesViewModel>> GetAllViewModelWithInclude();
        Task<BeneficiariesViewModel> GetPostViewModelById(int id);
        Task<List<BeneficiariesViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
