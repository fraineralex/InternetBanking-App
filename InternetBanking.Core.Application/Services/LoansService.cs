using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class LoansService : GenericService<SaveLoansViewModel, LoansViewModel, Loans>, ILoansService
    {
        private readonly ILoansRepository _loansRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public LoansService(ILoansRepository loansRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(loansRepository, mapper)
        {
            _loansRepository = loansRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }
    }
}
