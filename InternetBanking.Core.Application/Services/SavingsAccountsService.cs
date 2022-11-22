using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Core.Application.ViewModels.Client;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class SavingsAccountsService : GenericService<SavingsAccountsSaveViewModel, SavingsAccountsViewModel, SavingsAccounts>, ISavingsAccountsService
    {
        private readonly ISavingsAccountsRepository _savingsAccountsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public SavingsAccountsService(ISavingsAccountsRepository savingsAccountsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(savingsAccountsRepository, mapper)
        {
            _savingsAccountsRepository = savingsAccountsRepository;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<SavingsAccountsViewModel>> GetAllSavingsAccountsViewModels(string customerId)
        {
            var savingsAccountsList = await _savingsAccountsRepository.GetAllAsync();

            return _mapper.Map<List<SavingsAccountsViewModel>>(savingsAccountsList).Where(account => account.CustomerId == customerId).ToList();
        }
    }
}
