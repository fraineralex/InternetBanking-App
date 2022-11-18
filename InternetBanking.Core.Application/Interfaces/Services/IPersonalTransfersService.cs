using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Application.ViewModels.Client;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPersonalTransfersService : IGenericService<PersonalTransfersSaveViewModel, PersonalTransfersViewModel, PersonalTransfers>
    {

    }    

    
}
