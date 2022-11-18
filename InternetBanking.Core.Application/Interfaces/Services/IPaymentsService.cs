using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPaymentsService : IGenericService<SavePaymentsViewModel, PaymentsViewModel, Payments>
    {
        Task<List<PaymentsViewModel>> GetAllViewModelWithInclude();
        Task<PaymentsViewModel> GetPostViewModelById(int id);
        Task<List<PaymentsViewModel>> GetPostViewModelByUserId(int id);
    }    

    
}
