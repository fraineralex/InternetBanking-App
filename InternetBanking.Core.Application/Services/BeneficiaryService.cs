using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Core.Application.ViewModels.BeneficiarySaveViewModel;
using InternetBanking.Core.Application.ViewModels.BeneficiaryViewModel;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<BeneficiarySaveViewModel, BeneficiaryViewModel, Beneficiary>, IBeneficiaryService
    {

        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper, IProductService productService, IUserService userService) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _productService = productService;
            _userService = userService;
        }
        public override async Task<List<BeneficiaryViewModel>> GetAllViewModel()
        {
            var beneficiaryList = _mapper.Map<List<BeneficiaryViewModel>>(await _beneficiaryRepository.GetAllAsync());
            var productList = await _productService.GetAllViewModel();
            var userList = await _userService.GetAllVm();

            foreach (var beneficiary in beneficiaryList)
            {
                beneficiary.Beneficiary = productList.Where(e => e.AccountNumber == beneficiary.BeneficiaryAccountNumber).FirstOrDefault();

                beneficiary.User = userList.Where(e => e.Id == beneficiary.UserId).FirstOrDefault();
            }

            return beneficiaryList;
        }

        public async Task<BeneficiarySaveViewModel> GetByIdAsync(int id)
        {
            Beneficiary beneficiary = await _beneficiaryRepository.GetByIdAsync(id);
            BeneficiarySaveViewModel beneficiaryViewModel = _mapper.Map<BeneficiarySaveViewModel>(beneficiary);
            return beneficiaryViewModel;
        }
    }

}

