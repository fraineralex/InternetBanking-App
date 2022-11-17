using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repository;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class LoansRepository : GenericRepository<Loans>, ILoansRepository
    {
        private readonly ApplicationContext _dbContext;

        public LoansRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
