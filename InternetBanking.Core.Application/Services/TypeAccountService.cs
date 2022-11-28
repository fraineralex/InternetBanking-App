using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class TypeAccountService : GenericService<TypeAccountSaveViewModel, TypeAccountViewModel, TypeAccount>, ITypeAccountService
    {
        private readonly ITypeAccountRepository _typeAccountService;
        private readonly IMapper _mapper;
        public TypeAccountService(ITypeAccountRepository typeAccountService, IMapper mapper): base(typeAccountService, mapper)
        {
            _typeAccountService = typeAccountService;
            _mapper = mapper;
        }

    }
}
