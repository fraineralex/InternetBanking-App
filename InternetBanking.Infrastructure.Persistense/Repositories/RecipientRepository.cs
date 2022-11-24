using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;


namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class RecipientRepository : GenericRepository<Beneficiary>, IRecipientRepository
    {
        private readonly ApplicationContext _db;
        public RecipientRepository(ApplicationContext db): base(db)
        {
            _db = db;
        }


    }
}
