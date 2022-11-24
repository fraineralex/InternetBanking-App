using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TypeAccountRepository : GenericRepository<TypeAccount>, ITypeAccountRepository
    {
        private readonly ApplicationContext _db;
        public TypeAccountRepository(ApplicationContext db): base(db)
        {
            _db = db;
        }

        //public override static  async Task<List<TypeAccount>> GetAllAsync()
        //{
        //    return await _dbContext.Set<TypeAccount>().ToListAsync();
        //}
    }
}
