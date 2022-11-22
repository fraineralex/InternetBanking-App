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
    public class PaymentsService : GenericService<PaymentsSaveViewModel, PaymentsViewModel, Payments>, IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public PaymentsService(IPaymentsRepository paymentsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(paymentsRepository, mapper)
        {
            _paymentsRepository = paymentsRepository;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }
    }
}
