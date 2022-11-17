using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repository;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CashAdvancesRepository : GenericRepository<CashAdvances>, ICashAdvancesRepository
    {
        private readonly ApplicationContext _dbContext;

        public CashAdvancesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
