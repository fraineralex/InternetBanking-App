﻿using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class CreditCardsService : GenericService<SaveCreditCardsViewModel, CreditCardsViewModel, CreditCards>, ICreditCardsService
    {
        private readonly ICreditCardsRepository _creditCardsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public CreditCardsService(ICreditCardsRepository creditCardsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(creditCardsRepository, mapper)
        {
            _creditCardsRepository = creditCardsRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
