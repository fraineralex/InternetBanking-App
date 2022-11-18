using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class CashAdvancesService : GenericService<SaveCashAdvancesViewModel, CashAdvancesViewModel, CashAdvances>, ICashAdvancesService
    {
        private readonly ICashAdvancesRepository _cashAdvancesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public CashAdvancesService(ICashAdvancesRepository cashAdvancesRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(cashAdvancesRepository, mapper)
        {
            _cashAdvancesRepository = cashAdvancesRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
