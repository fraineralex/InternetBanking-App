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
    public class CashAdvancesService : GenericService<CashAdvancesSaveViewModel, CashAdvancesViewModel, CashAdvances>, ICashAdvancesService
    {
        private readonly ICashAdvancesRepository _cashAdvancesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public CashAdvancesService(ICashAdvancesRepository cashAdvancesRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(cashAdvancesRepository, mapper)
        {
            _cashAdvancesRepository = cashAdvancesRepository;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }
    }
}
