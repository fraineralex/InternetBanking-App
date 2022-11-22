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
    public class CreditCardsService : GenericService<CreditCardsSaveViewModel, CreditCardsViewModel, CreditCards>, ICreditCardsService
    {
        private readonly ICreditCardsRepository _creditCardsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public CreditCardsService(ICreditCardsRepository creditCardsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(creditCardsRepository, mapper)
        {
            _creditCardsRepository = creditCardsRepository;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<CreditCardsViewModel>> GetAllCreditCardsViewModels(string customerId)
        {
            var creditCardsList = await _creditCardsRepository.GetAllAsync();

            return _mapper.Map<List<CreditCardsViewModel>>(creditCardsList).Where(card => card.CustomerId == customerId).ToList();
        }
    }
}
