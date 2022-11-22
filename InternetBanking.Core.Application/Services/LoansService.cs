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
    public class LoansService : GenericService<LoansSaveViewModel, LoansViewModel, Loans>, ILoansService
    {
        private readonly ILoansRepository _loansRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public LoansService(ILoansRepository loansRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(loansRepository, mapper)
        {
            _loansRepository = loansRepository;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<LoansViewModel>> GetAllLoansViewModels(string customerId)
        {
            var loansList = await _loansRepository.GetAllAsync();

            return _mapper.Map<List<LoansViewModel>>(loansList).Where(loan => loan.CustomerId == customerId).ToList();
        }
    }
}
