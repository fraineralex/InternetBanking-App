using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repository;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CreditCardsRepository : GenericRepository<CreditCards>, ICreditCardsRepository
    {
        private readonly ApplicationContext _dbContext;

        public CreditCardsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
