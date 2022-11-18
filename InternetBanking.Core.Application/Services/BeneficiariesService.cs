using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiariesService : GenericService<SaveBeneficiariesViewModel, BeneficiariesViewModel, Beneficiaries>, IBeneficiariesService
    {
        private readonly IBeneficiariesRepository _beneficiariesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public BeneficiariesService(IBeneficiariesRepository beneficiariesRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(beneficiariesRepository, mapper)
        {
            _beneficiariesRepository = beneficiariesRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
