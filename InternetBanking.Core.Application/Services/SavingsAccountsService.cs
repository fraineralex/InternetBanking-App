using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class SavingsAccountsService : GenericService<SaveSavingsAccountsViewModel, SavingsAccountsViewModel, SavingsAccounts>, ISavingsAccountsService
    {
        private readonly ISavingsAccountsRepository _savingsAccountsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public SavingsAccountsService(ISavingsAccountsRepository savingsAccountsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(savingsAccountsRepository, mapper)
        {
            _savingsAccountsRepository = savingsAccountsRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
