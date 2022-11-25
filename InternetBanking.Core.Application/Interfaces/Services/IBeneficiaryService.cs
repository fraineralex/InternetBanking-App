using InternetBanking.Core.Application.ViewModels.BeneficiarySaveViewModel;
using InternetBanking.Core.Application.ViewModels.BeneficiaryViewModel;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService : IGenericService<BeneficiarySaveViewModel, BeneficiaryViewModel, Beneficiary>
    {
        Task<BeneficiarySaveViewModel> GetByIdAsync(int id);
    }
}
