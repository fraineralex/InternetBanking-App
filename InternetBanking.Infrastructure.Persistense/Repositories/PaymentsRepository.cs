using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PaymentsRepository : GenericRepository<Payments>, IPaymentsRepository
    {
        private readonly ApplicationContext _dbContext;

        public PaymentsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
