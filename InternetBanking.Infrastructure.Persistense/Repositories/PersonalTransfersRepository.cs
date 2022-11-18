using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PersonalTransfersRepository : GenericRepository<PersonalTransfers>, IPersonalTransfersRepository
    {
        private readonly ApplicationContext _dbContext;

        public PersonalTransfersRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
