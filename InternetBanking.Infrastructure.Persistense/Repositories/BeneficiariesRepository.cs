using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repository;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiariesRepository : GenericRepository<Beneficiaries>, IBeneficiariesRepository
    {
        private readonly ApplicationContext _dbContext;

        public BeneficiariesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
