using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentsService : GenericService<SavePaymentsViewModel, PaymentsViewModel, Payments>, IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public PaymentsService(IPaymentsRepository paymentsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(paymentsRepository, mapper)
        {
            _paymentsRepository = paymentsRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
