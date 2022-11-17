using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _applicationContext;

        public GenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {

            await _applicationContext.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _applicationContext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {

            return await _applicationContext.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<List<Entity>> GetWithIncludeAsync(List<string> properties)
        {

            var query = _applicationContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {

            var response = await _applicationContext.Set<Entity>().FindAsync(id);

            _applicationContext.Entry(response).CurrentValues.SetValues(entity);

            await _applicationContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(Entity entity)
        {

            _applicationContext.Set<Entity>().Remove(entity);
            await _applicationContext.SaveChangesAsync();

        }

        public Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            throw new NotImplementedException();
        }
    }
}
