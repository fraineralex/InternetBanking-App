using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class SavingsAccountsRepository : GenericRepository<SavingsAccounts>, ISavingsAccountsRepository
    {
        private readonly ApplicationContext _dbContext;

        public SavingsAccountsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
